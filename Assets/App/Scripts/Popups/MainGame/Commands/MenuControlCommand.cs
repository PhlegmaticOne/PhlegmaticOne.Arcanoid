using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Popups.MainGameMenu;

namespace Popups.MainGame.Commands
{
    public class MenuControlCommand : EmptyCommandBase
    {
        private readonly IGame _game;
        private readonly IPopupManager _popupManager;

        public MenuControlCommand(IGame game, IPopupManager popupManager)
        {
            _game = game;
            _popupManager = popupManager;
        }

        protected override void Execute()
        {
            _game.Pause();
            _popupManager.SpawnPopup<MainGameMenuPopup>();
        }
    }
}