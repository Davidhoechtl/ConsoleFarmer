
using ConsoleGame.HUD;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;
using System.Drawing;

namespace ConsoleGame.Buildings
{
    internal class RessourceSpawner : BuildingBase
    {
        public override string Name => "Spawner";
        public override BuildingType Type => BuildingType.Spawner;
        public int SpawnRateInSeconds => 10 / Level;
        public override bool IsBoostable => false;

        public int SpawnRadius = 3;

        public RessourceSpawner(int posX, int posY) 
            :base(posX, posY)
        {
        }

        public override IEnumerable<IRenderable> Update(GameState context)
        {
            Map map = context.map;
            if (lastSpawn == null || DateTime.Now > lastSpawn + new TimeSpan(0, 0, SpawnRateInSeconds))
            {
                //int rndX = rnd.Next(1, map.Width - 1);
                //int rndY = rnd.Next(1, map.Height - 1);
                Point rndPoint = GetRandomPointInRectangle(
                    new Rectangle(
                        PositionX - SpawnRadius,
                        PositionY - SpawnRadius,
                        SpawnRadius * 2,
                        SpawnRadius * 2
                    ),
                    map
                );

                Cell toChange = map[rndPoint.X, rndPoint.Y];
                toChange.Value = MapSymbols.Ressource;
                toChange.RessourceValue = GetRessourceValue();
                
                lastSpawn = DateTime.Now;

                return new List<IRenderable>() { this, map, context.GetMenuItemByType<RessourceCounter>() };
            }

            return new List<IRenderable>();
        }

        public override void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.ForegroundColor = GetConsoleColorFromLevel();
            Console.Write(MapSymbols.RessourceSpawner);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private Point GetRandomPointInRectangle(Rectangle rect, Map map)
        {
            int rndX = rnd.Next(rect.X, rect.X + rect.Width);
            int rndY = rnd.Next(rect.Y, rect.Y + rect.Height);

            if(rndX <= 1)
            {
                rndX++;
            }
            if(rndY <= 1)
            {
                rndY++;
            }
            if(rndX >= map.Width - 1)
            {
                rndX--;
            }
            if(rndY >= map.Height - 1)
            {
                rndY--;
            }

            return new Point(rndX, rndY);
        }

        private int GetRessourceValue()
        {
            switch (Level)
            {
                case 1:
                    return rnd.Next(1, 2);
                case 2:
                    return rnd.Next(2, 4);
                case 3:
                    return rnd.Next(3, 6);
                case 4:
                    return rnd.Next(4, 8);
                case 5:
                    return rnd.Next(6, 10);
                default:
                    throw new Exception("Unexcpected Level.");
            }
        }

        private DateTime? lastSpawn;
        private static Random rnd = new Random();
    }
}
