
namespace ConsoleGame
{
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;

    internal class GameLauncher
    {
        public void Launch()
        {
            Game game = new Game();
            GameStateManager stateManager = new();

            int choice = ShowMainMenu(stateManager);
            switch (choice)
            {
                case 0:
                    PreItemSelectActions();
                    game.Init(40, 20);
                    game.Start();
                    break;
                case 1:
                    if (stateManager.SaveFileAvailable())
                    {
                        stateManager.DeleteSaveFile();
                    }
                    PreItemSelectActions();
                    game.Init(40, 20);
                    game.Start();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Launch();
                    break;
            }
        }

        private int ShowMainMenu(GameStateManager stateManager)
        {
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
   _____                      _        ______                             
  / ____|                    | |      |  ____|                            
 | |     ___  _ __  ___  ___ | | ___  | |__ __ _ _ __ _ __ ___   ___ _ __ 
 | |    / _ \| '_ \/ __|/ _ \| |/ _ \ |  __/ _` | '__| '_ ` _ \ / _ \ '__|
 | |___| (_) | | | \__ \ (_) | |  __/ | | | (_| | |  | | | | | |  __/ |   
  \_____\___/|_| |_|___/\___/|_|\___| |_|  \__,_|_|  |_| |_| |_|\___|_|   
            ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            if (stateManager.SaveFileAvailable())
            {
                Console.WriteLine("0. Load Game");
                Console.WriteLine("1. Start new Game");
            }
            else
            {
                Console.WriteLine("1. Start Game");
            }

            Console.WriteLine("2. Exit");

            Console.Write("Enter your choice: ");
            bool success = int.TryParse(Console.ReadLine(), out int choice);
            if (success)
            {
                Console.CursorVisible = false;
                return choice;
            }
            else
            {
                Console.Clear();
                return ShowMainMenu(stateManager);
            }
        }

        private void PreItemSelectActions()
        {
            SoundManager.PlayMenuItemSelected();
            Console.Clear();
        }
    }
}
