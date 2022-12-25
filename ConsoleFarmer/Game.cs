using ConsoleGame.Achievement;
using ConsoleGame.Buildings;
using ConsoleGame.HUD;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;
using System.Reflection;

namespace ConsoleGame
{
    /// <summary>
    /// Class that manages game logic
    /// -> Game Loop
    /// </summary>
    internal class Game
    {
        public Map Map => currentGameState.map;
        public Player Player => currentGameState.player;
        public List<BuildingBase> Buildings => currentGameState.buildings;

        public void Init(int width, int height)
        {
            buildingFactory = new BuildingFactory();
            keyBuffer = new ConsoleKeyBuffer();

            GameStateManager stateManager = new();
            currentGameState = stateManager.TryLoadStateFromFile();
            if (currentGameState == null)
            {
                currentGameState = stateManager.CreateNewGameState(width, height, buildingFactory);
                questManager = new QuestManager();
            }
            else
            {
                questManager = new QuestManager(currentGameState.Quests);
            }

            menuItems = new List<IMenuItem>()
            {
                new CommandLine(1, height + 2, buildingFactory),
                new RessourceCounter(width + 4, 2),
                new RessourceGainViewer(width+4, 3),
                new BuildingOptions(width + 4, 6),
                new UpgradeOption(width + 4, 12),
                new DeleteOption(width + 4, 14),
                new CurrentQuestDisplay(width + 4, 16, questManager)
            };

            EventOutput.PositionX = width + 40;
            EventOutput.PositionY = 2;

            currentGameState.menuItems = menuItems;
        }

        public void Start()
        {
            StartGameTimer();
            RenderAll();
            while (true)
            {
                keyBuffer.ReadConsoleBuffer();
                IEnumerable<IRenderable> needToRefresh = Update();
                Render(needToRefresh);
            }
        }

        public IEnumerable<IRenderable> Update()
        {
            List<IRenderable> itemsToRefresh = new();

            menuItems.ForEach(item => itemsToRefresh.AddRange(item.Update(currentGameState)));
            Buildings.ForEach(building => itemsToRefresh.AddRange(building.Update(currentGameState)));
            itemsToRefresh.AddRange(Player.Update(currentGameState));

            questManager.Update(currentGameState);

            return itemsToRefresh.Distinct();
        }

        public void Render(IEnumerable<IRenderable> itemsForRefresh)
        {
            foreach (IRenderable item in itemsForRefresh)
            {
                if (item is Map)
                {
                    RenderAll();
                }
                else
                {
                    item.Render();
                }
            }
        }

        private void StartGameTimer()
        {
            Thread timerThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    currentGameState.Statistics.TimePlayed += new TimeSpan(0, 0, 1);
                }
            }));

            timerThread.Start();
        }

        private void RenderAll()
        {
            menuItems.ForEach(item => item.Render());
            Map.Render();
            Buildings.ForEach(building => building.Render());
            Player.Render();
        }

        private List<IMenuItem> menuItems;
        private GameState currentGameState;
        private ConsoleKeyBuffer keyBuffer;
        private QuestManager questManager;
        private BuildingFactory buildingFactory;
    }
}
