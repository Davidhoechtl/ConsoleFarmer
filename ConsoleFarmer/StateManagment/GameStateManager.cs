using ConsoleGame.Buildings;
using ConsoleGame.Infrastructure;
using Newtonsoft.Json;
using System.Numerics;

namespace ConsoleGame.StateManagment
{
    internal class GameStateManager
    {
        public readonly string FullSavePath = Path.Combine(Environment.CurrentDirectory, "save.txt");

        public void Save( GameState gameState )
        {
            SaveContext saveContext = GetSaveContextFromGameState(gameState);
            string json = JsonConvert.SerializeObject(saveContext);

            string fullSavePath = Path.Combine(FullSavePath);
            StreamWriter writer = new StreamWriter(fullSavePath);
            writer.WriteLine(json);
            writer.Close();
        }

        public GameState TryLoadStateFromFile()
        {
            try
            {
                StreamReader reader = new StreamReader(FullSavePath);
                string json = reader.ReadToEnd();
                SaveContext context = JsonConvert.DeserializeObject<SaveContext>(json, new QuestJsonConverter(), new BuildingBaseJsonConverter());
                return GetGameStateFromSaveContext(context);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public GameState CreateNewGameState(int mapWidth, int mapHeight, BuildingFactory buildingFactory )
        {
            List<BuildingBase> buildings = new List<BuildingBase>()
            {
                buildingFactory.CreateBuilding(BuildingType.Spawner, (mapWidth/2) + 1, (mapHeight/2) + 1)
            };

            Player player = new Player(mapWidth / 2, mapHeight / 2);
            Map map = new Map(mapWidth, mapHeight);
            map.Init();

            return new GameState()
            {
                player = player,
                buildings = buildings,
                map = map,
                Statistics = new()
            };
        }

        public void DeleteSaveFile()
        {
            File.Delete(FullSavePath);
        }

        public bool SaveFileAvailable()
        {
            return File.Exists(FullSavePath);
        }

        private GameState GetGameStateFromSaveContext(SaveContext saveContext)
        {
            Cell[,] cellsFlattened = Get2dArrayFromFlattenedList(saveContext.mapCells);
            saveContext.map.Cells = cellsFlattened;

            return new GameState()
            {
                player = saveContext.player,
                buildings = saveContext.buildings,
                map = saveContext.map,
                Statistics = saveContext.stats,
                Quests = saveContext.quests,
            };
        }

        private SaveContext GetSaveContextFromGameState(GameState state)
        {
            List<Cell> cellsFlattened = FlattenCells(state.map.Cells);

            return new SaveContext()
            {
                player = state.player,
                buildings = state.buildings,
                map = state.map,
                mapCells = cellsFlattened,
                stats = state.Statistics,
                quests= state.Quests
            };
        }

        private List<Cell> FlattenCells(Cell[,] cells)
        {
            List<Cell> cellsFlattened = new();
            for (int x = 0; x <= cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= cells.GetUpperBound(1); y++)
                {
                    cellsFlattened.Add(cells[x, y]);
                }
            }
            return cellsFlattened;
        }

        private Cell[,] Get2dArrayFromFlattenedList(List<Cell> cellsFlattened)
        {
            int width = cellsFlattened.Max(c => c.X) + 1;
            int height = cellsFlattened.Max(c => c.Y) + 1;

            Cell[,] cells = new Cell[width, height];
            foreach(Cell cell in cellsFlattened)
            {
                cells[cell.X, cell.Y] = cell;
            }

            return cells;
        }
    }
}
