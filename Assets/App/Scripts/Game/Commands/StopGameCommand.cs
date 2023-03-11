using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class StopGameCommand : ICommand
    {
        private readonly IGame _game;

        public StopGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Stop();
    }
}