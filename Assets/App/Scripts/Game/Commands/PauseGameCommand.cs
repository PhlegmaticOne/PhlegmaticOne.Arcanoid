using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class PauseGameCommand : ICommand
    {
        private readonly IGame _game;

        public PauseGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Pause();
    }
}