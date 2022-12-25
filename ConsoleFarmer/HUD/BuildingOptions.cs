using ConsoleGame.Buildings;

namespace ConsoleGame.HUD
{
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;
    using System;
    using System.Collections.Generic;

    internal class BuildingOptions : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }
        public int Count { get; private set; }

        public BuildingOptions(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Render()
        {
            int rowIndex = 0;

            Console.SetCursorPosition(PositionX, PositionY);
            Console.WriteLine("Building Menu:");

            foreach(KeyValuePair<BuildingType, int> pair in buildingCosts)
            {
                Console.SetCursorPosition(PositionX, PositionY + rowIndex + 1);

                Console.WriteLine($"-> {Enum.GetName(pair.Key)} (Cost: {pair.Value})");
                rowIndex++;
            }
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            foreach (BuildingType type in Enum.GetValues<BuildingType>())
            {
                buildingCosts[type] = CostCalculator.CalculateBuildingCost(type, context.buildings);
            }
            return new List<IRenderable>();
        }

        Dictionary<BuildingType, int> buildingCosts = new();
    }
}
