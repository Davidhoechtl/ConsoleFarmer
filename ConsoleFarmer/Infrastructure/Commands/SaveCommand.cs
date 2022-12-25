
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.HUD;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;

    internal class SaveCommand : ICommand
    {
        public string Name => "Save";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public SaveCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            Console.CursorVisible = true;
            OutputCommandMessage.Invoke("Wollen Sie das spiel speichern (y/n): ");
            string input = Console.ReadLine();
            SoundManager.PlayCommandEntered();
            if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                GameStateManager stateManager = new();
                stateManager.Save(context);
                EventOutput.AddMessage("GAME SAVED");
                SoundManager.PlayCommandEntered();
            }

            ClearCommandLine.Invoke();
            Console.CursorVisible = false;

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            OutputCommandMessage.Invoke("save");
        }
    }
}
