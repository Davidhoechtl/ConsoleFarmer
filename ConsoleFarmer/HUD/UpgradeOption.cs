
namespace ConsoleGame.HUD
{
    using ConsoleGame.Buildings;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;

    internal class UpgradeOption : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }
        public BuildingBase Current { get; set; }
        public int Cost => CostCalculator.CalculateUpgradeCost(Current);

        public UpgradeOption(int posX, int posY)
        {
            PositionX = posX;
            PositionY = posY;
        }

        public void Render()
        {
            if(Current != null)
            {
                Console.SetCursorPosition(PositionX, PositionY);
                if(Current.Level == BuildingBase.MaxLevel)
                {
                    Console.Write($"Upgrade: {Current.Name} Lv. MAX");
                }
                else
                {
                    Console.Write($"Upgrade: {Current.Name} Lv. {Current.Level} (cost: {Cost})");
                }
            }
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            BuildingBase buildingUnderPlayer = context.player.BuildingUnderPlayer;

            if(buildingUnderPlayer != null && Current != buildingUnderPlayer)
            {
                Current = buildingUnderPlayer;
                Clear(context);
                return new List<IRenderable>() { this };
            }
            else if(Current != null && buildingUnderPlayer == null)
            {
                Current = null;
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
