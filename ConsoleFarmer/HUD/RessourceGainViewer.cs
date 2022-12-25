
namespace ConsoleGame.HUD
{
    using ConsoleGame.Buildings;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class RessourceGainViewer : IMenuItem
    {
        public int PositionX {get; init;}
        public int PositionY { get; init;}

        public DateTime LastCheck { get; private set; } = DateTime.Now;
        public double GainPerSecond { get; private set; }

        public RessourceGainViewer(int posX, int posY)
        {
            PositionX = posX;
            PositionY = posY;
        }

        public void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($">Ressource Gain: {Math.Round(GainPerSecond, 2)}/second");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            List<IHasRessourceGainOverTime> ressourceGainers = context.buildings
                .Where(b => b is IHasRessourceGainOverTime)
                .Select(f => f as IHasRessourceGainOverTime)
                .ToList();

            double gainSum = 0;
            foreach(IHasRessourceGainOverTime ressourceGainer in ressourceGainers)
            {
                double gain = ressourceGainer.CurrentGainPerTick;
                double gainPerSecond = gain / (double)ressourceGainer.TimeToNextTickInSeconds;
                gainSum += gainPerSecond;
            }

            if(gainSum != GainPerSecond)
            {
                GainPerSecond = gainSum;
                Clear();
                return new List<IRenderable>() { this };
            }

            return new List<IRenderable>();
        }

        private void Clear()
        {
            // Hack
            for (int i = PositionX + 1; i < EventOutput.PositionX; i++)
            {
                Console.SetCursorPosition(i, PositionY);
                Console.Write(' ');
            }
        }
    }
}
