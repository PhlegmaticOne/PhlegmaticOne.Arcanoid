using Common.Scenes;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Popups.PackChoose;

namespace Popups.Common.Commands
{
    public class BackControlCommand : EmptyCommandBase
    {
        private readonly IGame _game;
        private readonly IPopupManager _popupManager;

        public BackControlCommand(IGame game, IPopupManager popupManager)
        {
            _game = game;
            _popupManager = popupManager;
        }
        
        protected override void Execute()
        {
            _game.Stop();
            var sceneChanger = new SceneChanger<PackChoosePopup>(_popupManager);
            sceneChanger.ChangeScene(SceneIndexes.MenuScene);
            _popupManager.CloseAllPopupsInstant();
        }
    }
}