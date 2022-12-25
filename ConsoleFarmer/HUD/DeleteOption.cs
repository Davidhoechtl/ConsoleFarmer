
namespace ConsoleGame.HUD
{
    using ConsoleGame.Buildings;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;
    using System;

    internal class DeleteOption : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }
        public BuildingBase Current { get; private set; }
        public int BuildingDeleteValue { get; private set; }

        public DeleteOption(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Render()
        {
            if(Current != null)
            {
                Console.SetCursorPosition(PositionX, PositionY);
                Console.Write($"Delete {Current.Name} and gain {BuildingDeleteValue}.");
            }
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            BuildingBase buildingUnderPlayer = context.player.BuildingUnderPlayer;
            if (buildingUnderPlayer != null && buildingUnderPlayer != Current)
            {
                Current = context.player.BuildingUnderPlayer;
                BuildingDeleteValue = CostCalculator.CalculateBuildingValue(Current, context.buildings) / 2;
                return new List<IRenderable>() { this };
            }
            else if(buildingUnderPlayer == null && buildingUnderPlayer != Current)
            {
                Current = null;
                BuildingDeleteValue = 0;
                Clear(context);
                return new List<IRenderable>() { this };
            }

            return new List<IRenderable>();
        }

        private void Clear(GameState context)
        {
            // Hack
            for (int column = PositionX; column < EventOutput.PositionX; column++)
            {
                Console.SetCursorPosition(column, PositionY);
                Console.Write(' ');
            }
        }
    }
}
