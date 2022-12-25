
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.HUD;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;

    internal class ExitCommand : ICommand
    {
        public string Name => "Exit";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public ExitCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            Console.CursorVisible = true;
            OutputCommandMessage.Invoke("Wollen Sie das spiel beenden (y/n): ");
            string input = Console.ReadLine();
            SoundManager.PlayCommandEntered();
            if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                GameStateManager stateManager = new();
                stateManager.Save(context);
                EventOutput.AddMessage("GAME SAVED");
                ClearCommandLine.Invoke();
                Environment.Exit(0);
            }

            ClearCommandLine.Invoke();
            Console.CursorVisible = false;

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            OutputCommandMessage.Invoke("exit");
        }
    }
}
