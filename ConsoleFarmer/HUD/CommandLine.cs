
namespace ConsoleGame.HUD
{
    using ConsoleGame.Infrastructure;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    internal class CommandLine : IMenuItem
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }
        public int Count { get; private set; }

        public CommandLine(int positionX, int positionY, BuildingFactory buildingFactory)
        {
            PositionX = positionX;
            PositionY = positionY;

            commandExecuter = new CommandExecuter(
                ShowCommandMessage, 
                ClearCommandLine, 
                buildingFactory
            );

            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(">Press 'c' to enter command line and type in 'help' to view commands");
        }

        public void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(">");
        }

        public IEnumerable<IRenderable> Update(GameState context)
        {
            return CheckForActions(context);
        }

        private IEnumerable<IRenderable> CheckForActions(GameState context)
        {
            if (ConsoleKeyBuffer.KeyAvailable)
            {
                if (CheckInput())
                {
                    ClearCommandLine();
                    Console.SetCursorPosition(PositionX + 1, PositionY);

                    Console.CursorVisible = true;
                    string input = Console.ReadLine();
                    Console.CursorVisible = false;

                    return commandExecuter.HandleCommandInput(context, input);
                }
            }

            return new List<IRenderable>();
        }

        private bool CheckInput()
        {
            ConsoleKeyInfo key = ConsoleKeyBuffer.GetNext(false);
            if (key.Key == ConsoleKey.C)
            {
                return true;
            }

            return false;
        }

        private void ClearCommandLine()
        {
            for (int i = PositionX + 1; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, PositionY);
                Console.Write(' ');
            }
        }

        private void ShowCommandMessage(string message)
        {
            ClearCommandLine();
            Console.SetCursorPosition(PositionX + 1, PositionY);
            Console.Write(message);
        }

        private readonly CommandExecuter commandExecuter;
    }
}
