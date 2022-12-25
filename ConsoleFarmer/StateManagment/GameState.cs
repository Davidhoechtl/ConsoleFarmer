using ConsoleGame.Achievement;
using ConsoleGame.Buildings;
using ConsoleGame.HUD;
using ConsoleGame.Infrastructure;

namespace ConsoleGame.StateManagment
{
    internal class GameState
    {
        public Player player;
        public Map map;
        public List<BuildingBase> buildings;
        public List<IMenuItem> menuItems;
        public GameStatistics Statistics;
        public List<Quest> Quests;
        public int RessourceCount => player.RessourceCount;

        public IMenuItem GetMenuItemByType<T>()
        {
            IMenuItem item = menuItems.FirstOrDefault(item => item.GetType() == typeof(T));
            if(item!=null)
            {
                return item;
            }
            else
            {
                throw new ArgumentException($"Menu item of type {typeof(T)} is not registered in game state");
            }
        }
    }
}
