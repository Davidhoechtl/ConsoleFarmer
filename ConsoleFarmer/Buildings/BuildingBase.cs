
namespace ConsoleGame.Buildings
{
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;
    using Newtonsoft.Json;

    [JsonConverter(typeof(BuildingBaseJsonConverter))]
    internal abstract class BuildingBase : IRenderable
    {
        public const int MaxLevel = 5;

        [JsonProperty]
        public int PositionX { get; private set; }
        [JsonProperty]
        public int PositionY { get; private set; }
        public int Level { get; set; } = 1;
        public abstract string Name { get; }
        public abstract BuildingType Type { get; }
        public abstract bool IsBoostable { get; }

        public BuildingBase(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public abstract IEnumerable<IRenderable> Update( GameState context );
        public abstract void Render();

        protected ConsoleColor GetConsoleColorFromLevel()
        {
            switch (Level)
            {
                case 2:
                    return ConsoleColor.Yellow;
                case 3:
                    return ConsoleColor.Green;
                case 4:
                    return ConsoleColor.DarkRed;
                case 5:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
