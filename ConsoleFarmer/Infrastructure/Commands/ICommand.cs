
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.StateManagment;

    internal interface ICommand
    {
        string Name { get; }
        Action<string> OutputCommandMessage { get; }
        Action ClearCommandLine { get; }
        IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args);
        void OutputCommandUsage();
    }
}
