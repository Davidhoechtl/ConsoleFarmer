
namespace ConsoleGame
{
    using ConsoleGame.Achievement;
    using ConsoleGame.Buildings;
    using ConsoleGame.HUD;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    using Newtonsoft.Json;
    using System.Numerics;

    internal class Player : IRenderable
    {
        [JsonProperty]
        public int XPosition { get; private set; }
        [JsonProperty]
        public int YPosition { get; private set; }
        public int RessourceCount 
        {
            get => ressourceCount;
            set
            {
                int deltaRessources = value - ressourceCount;
                if(deltaRessources > 0)
                {
                    GameStatistics.RecourssenGathered += deltaRessources;
                }

                ressourceCount = value;
            }
        }

        [JsonIgnore]
        public BuildingBase BuildingUnderPlayer { get; private set; }

        public Player(int startX, int startY)
        {
            XPosition = startX;
            YPosition = startY;

#if DEBUG
            ressourceCount = 100000;
#endif
        }

        public IEnumerable<IRenderable> Update(GameState state)
        {
            Map map = state.map;
            if (ConsoleKeyBuffer.KeyAvailable)
            {
                ConsoleKeyInfo key = ConsoleKeyBuffer.GetNext(true);
                Vector2 direction = new Vector2(0, 0);
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        if (map[XPosition, YPosition - 1].Value != MapSymbols.Wall)
                        {
                            direction.Y--;
                        }
                        break;
                    case ConsoleKey.A:
                        if (map[XPosition - 1, YPosition].Value != MapSymbols.Wall)
                        {
                            direction.X--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (map[XPosition, YPosition + 1].Value != MapSymbols.Wall)
                        {
                            direction.Y++;
                        }
                        break;
                    case ConsoleKey.D:
                        if (map[XPosition + 1, YPosition].Value != MapSymbols.Wall)
                        {
                            direction.X++;
                        }
                        break;
                    default:
                        break;
                }

                if(direction.Length() > 0)
                {
                    Console.SetCursorPosition(XPosition, YPosition);
                    Console.Write(' ');

                    XPosition = XPosition + (int)direction.X;
                    YPosition = YPosition + (int)direction.Y;
                    state.Statistics.CellsMoved++;

                    Render();
                }

                ConsoleKeyBuffer.Flush();

                List<IRenderable> needToRefresh = new() { this };
                if (map[XPosition, YPosition].Value == MapSymbols.Ressource)
                {
                    Cell ressourceCell = map[XPosition, YPosition];
                    SoundManager.PlayRessourceCollected();
                    EventOutput.AddMessage($"Player found {ressourceCell.RessourceValue} Ressources!");
                    RessourceCount += ressourceCell.RessourceValue;
                    ressourceCell.Value = MapSymbols.Empty;
                    ressourceCell.RessourceValue = 0;

                    needToRefresh.Add(state.GetMenuItemByType<CurrentQuestDisplay>());
                }

                if(BuildingUnderPlayer != null )
                {
                    needToRefresh.Add(BuildingUnderPlayer);
                }

                BuildingUnderPlayer = GetBuildingUnderPlayer(state);

                return needToRefresh;
            }

            return new List<IRenderable>();
        }

        public void Render()
        {
            Console.SetCursorPosition(XPosition, YPosition);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(MapSymbols.Player);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private BuildingBase GetBuildingUnderPlayer(GameState context)
        {
            Player player = context.player;
            BuildingBase buildingUnderPlayer = context.buildings.FirstOrDefault(building =>
                building.PositionX == player.XPosition && building.PositionY == player.YPosition
            );

            return buildingUnderPlayer;
        }

        private int ressourceCount;
    }
}
