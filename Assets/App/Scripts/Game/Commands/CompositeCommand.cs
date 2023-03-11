using System;
using System.Collections;
using System.Collections.Generic;
using Game.Commands.Base;

namespace Game.Commands
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