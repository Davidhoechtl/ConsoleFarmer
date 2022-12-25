
namespace ConsoleGame.Infrastructure
{
    using ConsoleGame.Buildings;

    internal static class CostCalculator
    {
        public static int CalculateBuildingCost(BuildingType type, List<BuildingBase> buildings)
        {
            int buildingCount = buildings.Count(b => b.Type == type);
            switch (type)
            {
                case BuildingType.Factory:
                    return 3 + (buildingCount * buildingCount * 10);
                case BuildingType.Booster:
                    return 10 + (buildingCount * buildingCount * 5);
                case BuildingType.Spawner:
                    return 5 + (buildingCount * buildingCount * 2);
                case BuildingType.Bank:
                    return 5000 + (buildingCount * buildingCount * 200);
                default:
                    throw new InvalidOperationException("Building not recognized.");
            }
        }

        public static int CalculateUpgradeCost(BuildingBase building)
        {
            if (building == null)
            {
                return 0;
            }

            switch (building.Type)
            {
                case BuildingType.Factory:
                    return GetUpgradeCost(10, 20, building.Level);
                case BuildingType.Booster:
                    return GetUpgradeCost(20, 40, building.Level);
                case BuildingType.Spawner:
                    return GetUpgradeCost(5, 10, building.Level);
                case BuildingType.Bank:
                    return GetUpgradeCost(1, 500, building.Level);
                default:
                    throw new InvalidOperationException("Building not recognized.");
            }
        }

        public static int CalculateBuildingValue(BuildingBase building, List<BuildingBase> buildings)
        {
            if (building == null)
            {
                return 0;
            }

            int value = 0;
            int buildingCount = buildings.Count(b => b.Type == building.Type) - 1;
            switch (building.Type)
            {
                case BuildingType.Factory:
                    value =  3 + (buildingCount * buildingCount * 10);
                    for (int i = 1; i <= building.Level; i++)
                    {
                        value += GetUpgradeCost(10, 20, i);
                    }
                    return value;
                case BuildingType.Booster:
                    value = 10 + (buildingCount * buildingCount * 5);
                    for (int i = 1; i <= building.Level; i++)
                    {
                        value += GetUpgradeCost(20, 40, i);
                    }
                    return value;
                case BuildingType.Spawner:
                    value = 5 + (buildingCount * buildingCount * 2);
                    for (int i = 1; i <= building.Level; i++)
                    {
                        value += GetUpgradeCost(5, 10, i);
                    }
                    return value;
                case BuildingType.Bank:
                    value = 5000 + (buildingCount * buildingCount * 200);
                    for (int i = 1; i <= building.Level; i++)
                    {
                        value += GetUpgradeCost(1, 500, i);
                    }
                    return value;
                default:
                    throw new InvalidOperationException("Building not recognized.");
            }
        }

        private static int GetUpgradeCost(int baseCost, int mulitplier, int buildingLevel)
        {
            return mulitplier * buildingLevel * buildingLevel + baseCost;
        }
    }
}
