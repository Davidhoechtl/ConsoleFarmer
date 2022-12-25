using ConsoleGame.HUD;
using ConsoleGame.Sounds;
using ConsoleGame.StateManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure.Commands
{
    internal class DeleteCommand : ICommand
    {
        public string Name => "Delete";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public DeleteCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            DeleteOption deleteOption = context.menuItems.FirstOrDefault(item => item is DeleteOption) as DeleteOption;
            if (deleteOption.Current != null)
            {
                context.buildings.Remove(deleteOption.Current);
                context.player.RessourceCount += deleteOption.BuildingDeleteValue;
                SoundManager.PlayCommandEntered();
                return new List<IRenderable>() { context.GetMenuItemByType<RessourceCounter>() };
            }
            else
            {
                ShowError("There is no building under the Player.");
            }

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            OutputCommandMessage.Invoke("delete");
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            OutputCommandMessage.Invoke(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
