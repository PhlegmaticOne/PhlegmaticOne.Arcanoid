using Common.Scenes;
using Game.Commands.Base;
using Libs.Popups.Base;
using Popups.PackChoose;

namespace Game.Commands
{
    public class BackToPacksMenuCommand : ICommand
    {
        private readonly IPopupManager _popupManager;

        public BackToPacksMenuCommand(IPopupManager popupManager) => _popupManager = popupManager;

        public void Execute()
        {
            var sceneChanger = new SceneChanger<PackChoosePopup>(_popupManager);
            sceneChanger.ChangeScene(SceneIndexes.MenuScene);
        }
    }
}