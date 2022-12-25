using ConsoleGame.HUD;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;

namespace ConsoleGame.Buildings
{
    internal class Bank : BuildingBase, IHasRessourceGainOverTime
    {
        public override string Name => "Bank";

        public override BuildingType Type => BuildingType.Bank;
        public double InterestMulitplier => 1.05 + ((double)Level / 10);

        public DateTime LastTick { get; private set; } = DateTime.Now;
        public int TimeToNextTickInSeconds => 30 - (Level*2);
        public int CurrentGainPerTick { get; private set; }

        public override bool IsBoostable => false;

        public Bank(int positionX, int positionY) : base(positionX, positionY)
        {
        }

        public override void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);

            Console.ForegroundColor = GetConsoleColorFromLevel();
            Console.Write(MapSymbols.Bank);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override IEnumerable<IRenderable> Update(GameState context)
        {
            if(DateTime.Now >= LastTick + new TimeSpan(0, 0, TimeToNextTickInSeconds))
            {
                int currentRessourceCount = context.RessourceCount;
                CurrentGainPerTick =  ((int)(currentRessourceCount * InterestMulitplier)) - currentRessourceCount;

                context.player.RessourceCount += CurrentGainPerTick;
                EventOutput.AddMessage($"Received {CurrentGainPerTick} interest");

                LastTick = DateTime.Now;
                return new List<IRenderable>() { this, context.GetMenuItemByType<RessourceCounter>(), context.GetMenuItemByType<RessourceGainViewer>() };
            }

            return new List<IRenderable>();
        }

    }
}
