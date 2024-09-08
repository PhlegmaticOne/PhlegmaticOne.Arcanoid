using Game.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class PauseGameCommand : EmptyCommandBase
    {
        private readonly IGame _game;
        public PauseGameCommand(IGame game) => _game = game;
        protected override void Execute() => _game.Pause();
    }
}