using ConsoleGame.Achievement;
using ConsoleGame.Buildings;
using ConsoleGame.Infrastructure;

namespace ConsoleGame.StateManagment
{
    internal class SaveContext
    {
        public Player player;
        public Map map;
        public List<BuildingBase> buildings;
        public List<Cell> mapCells;
        public GameStatistics stats;
        public List<Quest> quests;
    }
}
