

namespace ConsoleGame.HUD
{
    using System;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;

    internal class RessourceCounter : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }
        public int Count { get; private set; }

        public RessourceCounter(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($">Ressources: {Count}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            int scoreUpdated = context.RessourceCount;

            if (scoreUpdated != Count)
            {
                Count = scoreUpdated;
                ClearCommandLine();
                return new List<IRenderable>() { this };
            }

            return new List<IRenderable>();
        }

        private void ClearCommandLine()
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
