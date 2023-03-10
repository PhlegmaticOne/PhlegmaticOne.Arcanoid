using Common.Scenes;
using Game.Base;
using Game.Commands.Base;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.PackChoose;

namespace Game.Commands
{
    public class BackToPacksMenuCommand : ICommand
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;
        private readonly IPopupManager _popupManager;

        public BackToPacksMenuCommand(IGame<MainGameData, MainGameEvents> mainGame,
            IPopupManager popupManager)
        {
            _mainGame = mainGame;
            _popupManager = popupManager;
        }
        
        public void Execute()
        {
            _mainGame.Stop();
            var sceneChanger = new SceneChanger<PackChoosePopup>(_popupManager);
            sceneChanger.ChangeScene(0);
        }
    }
}