using Game.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class PauseGameCommand : ICommand
    {
        private readonly IGame _game;

        public PauseGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Pause();
    }
}