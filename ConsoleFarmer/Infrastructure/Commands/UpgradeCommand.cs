
namespace ConsoleGame.Infrastructure.Commands
{
    using ConsoleGame.Buildings;
    using ConsoleGame.HUD;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;

    internal class UpgradeCommand : ICommand
    {
        public string Name => "Upgrade";

        public Action<string> OutputCommandMessage { get; init; }

        public Action ClearCommandLine { get; init; }

        public UpgradeCommand(Action<string> outputCommandAction, Action clearCommandAction)
        {
            OutputCommandMessage = outputCommandAction;
            ClearCommandLine = clearCommandAction;
        }

        public IEnumerable<IRenderable> ExecuteCommand(GameState context, params string[] args)
        {
            UpgradeOption upgradeOption = context.menuItems.FirstOrDefault(item => item is UpgradeOption) as UpgradeOption;
            if (upgradeOption.Current != null && upgradeOption.Current.Level < BuildingBase.MaxLevel)
            {
                if (context.player.RessourceCount >= upgradeOption.Cost)
                {
                    context.player.RessourceCount -= upgradeOption.Cost;
                    upgradeOption.Current.Level++;
                    upgradeOption.Current = null;
                    SoundManager.PlayCommandEntered();
                    return new List<IRenderable>() {
                        context.GetMenuItemByType<RessourceCounter>(),
                        context.GetMenuItemByType<UpgradeOption>(),
                        context.GetMenuItemByType<BuildingOptions>(),
                        context.GetMenuItemByType<DeleteOption>()
                    };
                }
            }
            else if(upgradeOption.Current == null)
            {
                ShowError("There is no building under the Player.");
            }
            else if(upgradeOption.Current.Level < BuildingBase.MaxLevel)
            {
                ShowError("Building is at max level.");
            }

            return new List<IRenderable>();
        }

        public void OutputCommandUsage()
        {
            OutputCommandMessage.Invoke("upgrade");
        }

        private void ShowError(string message)
        {
            SoundManager.PlayCommandEnteredFailed();
            Console.ForegroundColor = ConsoleColor.Red;
            OutputCommandMessage.Invoke(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
