using Game.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class ContinueGameCommand : ICommand
    {
        private readonly IGame _game;

        public ContinueGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Unpause();
    }
}