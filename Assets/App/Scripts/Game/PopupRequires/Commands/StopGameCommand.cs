using Game.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class StopGameCommand : ICommand
    {
        private readonly IGame _game;

        public StopGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Stop();
    }
}