
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.Buildings;
    using ConsoleGame.HUD;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;

    internal class BuildCommand : ICommand
    {
        public string Name => "Build";

        public Action<string> OutputCommandMessage { get; init; }
        public Action ClearCommandLine { get; init; }

        public BuildCommand(Action<string> outputCommandAction, Action clearCommandAction, BuildingFactory buildingFactory)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;

            this.buildingFactory = buildingFactory;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            if(args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                OutputCommandUsage();
                return new List<IRenderable>();
            }

            string buildingName = args[0];

            bool interceptsWithOther = InterceptsWithOtherBuilding(context.buildings, context.player);
            if (interceptsWithOther)
            {
                return new List<IRenderable>();
            }

            // get building by name
            BuildingType? buildingType = null;
            foreach (BuildingType type in Enum.GetValues<BuildingType>())
            {
                if (Enum.GetName(type).Equals(buildingName, StringComparison.OrdinalIgnoreCase))
                {
                    buildingType = type;
                }
            }

            if (!buildingType.HasValue)
            {
                ShowError($"No Building named '{buildingName}' was found");
                Console.CursorVisible = false;
                return new List<IRenderable>();
            }

            //check if building may be placed
            Point buildingLocation = new Point(context.player.XPosition, context.player.YPosition);
            switch (buildingType.Value)
            {
                case BuildingType.Booster:
                    int maxdistanceToOtherBooster = 2;
                    foreach (Booster booster in context.buildings.Where(b => b is Booster).Select(b => b as Booster))
                    {
                        int distanceToBuildingLocation = LocationHelper.GetDistanceToBuilding(buildingLocation, booster);
                        if (maxdistanceToOtherBooster > distanceToBuildingLocation)
                        {
                            ShowError($"Too near to other Booster (max 1 Booster every {maxdistanceToOtherBooster} tiles)");
                            Console.CursorVisible = false;
                            return new List<IRenderable>();
                        }
                    }
                    break;
            }

            int cost = CostCalculator.CalculateBuildingCost(buildingType.Value, context.buildings);

            if (context.player.RessourceCount >= cost)
            {
                context.player.RessourceCount -= cost;
                context.Statistics.BuildingsPlaced++;

                EventOutput.AddMessage($"{Enum.GetName(buildingType.Value)} was built");

                // add Buidling
                BuildingBase boughtBuilding = buildingFactory.CreateBuilding(buildingType.Value, context.player.XPosition, context.player.YPosition);
                context.buildings.Add(
                    boughtBuilding
                );

                ClearCommandLine.Invoke();
                SoundManager.PlayCommandEntered();
                return new List<IRenderable>() { boughtBuilding, context.GetMenuItemByType<RessourceCounter>(), context.GetMenuItemByType<BuildingOptions>() };
            }

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            SoundManager.PlayCommandEnteredFailed();
            ShowError("build <buildingName>");
        }

        private bool InterceptsWithOtherBuilding(List<BuildingBase> buildings, Player player)
        {
            return buildings.Any(b => b.PositionX == player.XPosition && b.PositionY == player.YPosition);
        }

        private void ShowError(string message)
        {
            SoundManager.PlayCommandEnteredFailed();

            Console.ForegroundColor = ConsoleColor.Red;
            OutputCommandMessage.Invoke(message);
            Console.ForegroundColor = ConsoleColor.White;
        }


        private readonly BuildingFactory buildingFactory;
    }
}
