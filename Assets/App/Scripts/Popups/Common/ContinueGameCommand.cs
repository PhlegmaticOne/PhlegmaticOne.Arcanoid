using Game.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class ContinueGameCommand : EmptyCommandBase
    {
        private readonly IGame _game;

        public ContinueGameCommand(IGame game) => _game = game;
        protected override void Execute() => _game.Unpause();
    }
}