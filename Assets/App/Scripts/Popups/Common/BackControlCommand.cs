using Common.Scenes;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Common.Packs.Views;

namespace Popups.Common
{
    public class BackControlCommand : EmptyCommandBase
    {
        private readonly IGame _game;
        private readonly IPopupManager _popupManager;
        private readonly ISceneChanger _sceneChanger;

        public BackControlCommand(IGame game, IPopupManager popupManager, ISceneChanger sceneChanger)
        {
            _game = game;
            _popupManager = popupManager;
            _sceneChanger = sceneChanger;
        }
        
        protected override void Execute()
        {
            _sceneChanger.OnOverlay += SceneChangerOnOnOverlay;
            _sceneChanger.SceneChanged += SceneChangerOnSceneChanged;
            _sceneChanger.ChangeScene(SceneNames.Menu);
        }

        private void SceneChangerOnOnOverlay()
        {
            _game.Stop();
        }

        private void SceneChangerOnSceneChanged()
        {
            _sceneChanger.OnOverlay -= SceneChangerOnOnOverlay;
            _sceneChanger.SceneChanged -= SceneChangerOnSceneChanged;
            _popupManager.SpawnPopup<PackChoosePopup>();
        }
    }
}