
namespace ConsoleGame.HUD
{
    using ConsoleGame.Achievement;
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;

    internal class CurrentQuestDisplay : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }

        public CurrentQuestDisplay(int positionX, int positionY, QuestManager questManager)
        {
            PositionX = positionX;
            PositionY = positionY;
            this.questManager = questManager;
        }

        public void Render()
        {
            if(currentQuest != null)
            {
                Clear();
                Console.SetCursorPosition(PositionX, PositionY);
                Console.WriteLine("Current Quest:");
                Console.SetCursorPosition(PositionX, PositionY + 1);
                Console.Write(currentQuest);
            }
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            Quest nextQuest = questManager.GetCurrentQuest();
            if (nextQuest != currentQuest)
            {
                currentQuest = nextQuest;
                return new List<IRenderable>() { this };
            }

            return new List<IRenderable>();
        }

        private void Clear()
        {
            for (int i = PositionX; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, PositionY + 1);
                Console.Write(' ');
            }
        }

        private Quest currentQuest = null;
        private readonly QuestManager questManager;
    }
}
