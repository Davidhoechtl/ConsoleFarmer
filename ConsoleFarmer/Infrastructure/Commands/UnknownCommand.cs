
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;

    internal class UnknownCommand : ICommand
    {
        public string Name => "Unknown";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public UnknownCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            SoundManager.PlayCommandEnteredFailed();
            Console.ForegroundColor = ConsoleColor.Red;
            OutputCommandMessage.Invoke("The Command is unknown. Type 'help' to see all Commands");
            Console.ForegroundColor = ConsoleColor.White;

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            throw new NotImplementedException();
        }
    }
}
