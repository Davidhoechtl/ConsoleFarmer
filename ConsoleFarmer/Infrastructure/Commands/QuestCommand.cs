namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.Achievement;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    using System.Text;

    internal class QuestCommand : ICommand
    {
        public string Name => "Quests";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public QuestCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            SoundManager.PlayCommandEntered();
            Console.CursorVisible = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Quests:");
            foreach (Quest quest in context.Quests)
            {
                sb.AppendLine(quest.ToString());
            }
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
            OutputCommandMessage.Invoke("quests");
        }
    }
}
