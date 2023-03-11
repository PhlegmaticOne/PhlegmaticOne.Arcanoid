using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class ContinueGameCommand : ICommand
    {
        private readonly IGame _game;

        public ContinueGameCommand(IGame game) => _game = game;

        public void Execute() => _game.Unpause();
    }
}