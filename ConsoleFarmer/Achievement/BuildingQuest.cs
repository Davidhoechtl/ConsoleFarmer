using ConsoleGame.Buildings;
using ConsoleGame.StateManagment;

namespace ConsoleGame.Achievement
{
    using System.Linq;

    internal class BuildingQuest : Quest
    {
        public int BuildingCount { get; init; }
        public BuildingType BuildingType { get; init; }

        public override QuestType Type => QuestType.Building;

        public BuildingQuest(string descritpion, int reward, int difficulty, int buildingCount, BuildingType type)
            :base(descritpion, reward, difficulty)
        {
            BuildingType = type;
            BuildingCount = buildingCount;
        }


        public override bool UpdateProgress(GameState state)
        {
            int currentBuildingCount = state.buildings.Count(b => b.Type.Equals(BuildingType));
            Progress = currentBuildingCount;
            if (currentBuildingCount >= BuildingCount)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Description} ({Progress}/{BuildingCount})";
        }
    }
}
