using ConsoleGame.Sounds;
using ConsoleGame.StateManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Name => "Help";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public HelpCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            SoundManager.PlayCommandEntered();

            Console.CursorVisible = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Actions:");
            sb.AppendLine("Building: \tbuilding <buildingName>");
            sb.AppendLine("Building: \tupgrade");
            sb.AppendLine("Statistics: \tstats");
            sb.AppendLine("Quests: \tquests");
            sb.AppendLine("Saving: \tsave");
            sb.AppendLine("Exit: \t\texit");
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
            OutputCommandMessage.Invoke("help");
        }

    }
}
