
namespace ConsoleGame.Infrastructure
{
    using ConsoleGame.Infrastructure.Commands;
    using ConsoleGame.Sounds;
    using ConsoleGame.StateManagment;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Media;

    internal class CommandExecuter
    {
        public CommandExecuter(Action<string> outputCommandMessage, Action clearCommandLine, BuildingFactory buildingFactory)
        {
            commands = new List<ICommand>()
            {
                new BuildCommand(outputCommandMessage, clearCommandLine, buildingFactory),
                new DeleteCommand(outputCommandMessage, clearCommandLine),
                new UpgradeCommand(outputCommandMessage, clearCommandLine),
                new ExitCommand(outputCommandMessage, clearCommandLine),
                new HelpCommand(outputCommandMessage, clearCommandLine),
                new QuestCommand(outputCommandMessage, clearCommandLine),
                new StatsCommand(outputCommandMessage, clearCommandLine),
                new SaveCommand(outputCommandMessage, clearCommandLine),
                new ExitCommand(outputCommandMessage, clearCommandLine),
                new UnknownCommand(outputCommandMessage, clearCommandLine)
            };

            this.clearCommandLine = clearCommandLine;
        }

        public IEnumerable<IRenderable> HandleCommandInput(GameState context, string input)
        {
            string[] commandData = input.Split(' ');
            clearCommandLine.Invoke();

            ICommand command = commands.FirstOrDefault(
                command => command.Name.Equals(commandData[0], StringComparison.OrdinalIgnoreCase),
                defaultValue: commands.First(c => c is UnknownCommand)
            );

            return command.ExecuteCommand(context, commandData.Skip(1).ToArray());
        }

        private List<ICommand> commands;
        private readonly Action clearCommandLine;
    }
}
