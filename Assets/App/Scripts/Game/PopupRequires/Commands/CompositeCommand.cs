using System.Collections.Generic;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class CompositeCommand : ICommand
    {
        private readonly IEnumerable<ICommand> _commands;

        public CompositeCommand(IEnumerable<ICommand> commands) => _commands = commands;

        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }
    }
}