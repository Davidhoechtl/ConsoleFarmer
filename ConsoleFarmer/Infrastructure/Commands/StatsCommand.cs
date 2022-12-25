
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    using System;
    using System.Text;
    
    internal class StatsCommand : ICommand
    {
        public string Name => "Stats";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public StatsCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            SoundManager.PlayCommandEntered();
            Console.CursorVisible = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Statistics: ");
            sb.AppendLine("Recoursses gathered: " + GameStatistics.RecourssenGathered);
            sb.AppendLine("Cells moved: " + context.Statistics.CellsMoved);
            sb.AppendLine("Buildings placed: " + context.Statistics.BuildingsPlaced);
            sb.AppendLine("Time played: " + context.Statistics.TimePlayed);
            sb.AppendLine("Press any key to conitnue...");

            OutputCommandMessage.Invoke(sb.ToString());
            Console.ReadKey();

            Console.CursorVisible = false;
            Console.Clear();

            // Hack
            return new List<IRenderable>() { context.map };
        }

        public void OutputCommandUsage()
        {
            OutputCommandMessage.Invoke("stats");
        }
    }
}
