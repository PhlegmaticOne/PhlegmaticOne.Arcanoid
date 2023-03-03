using Libs.Popups;
using Libs.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGamePopup : Popup
    {
        [SerializeField] private Button _menuButton;

        private IPopupManager _popupManager;
        private Game.MainGame _mainGame;

        public void Initialize(IPopupManager popupManager, Game.MainGame mainGame)
        {
            _mainGame = mainGame;
            _popupManager = popupManager;
            ConfigureMenuButton();
        }

        protected override void OnShow()
        {
            _mainGame.StartGame();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_menuButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_menuButton);
        }

        private void ConfigureMenuButton()
        {
            _menuButton.onClick.AddListener(() => _popupManager.SpawnPopup<MainGameMenuPopup>());
        }

        public override void Reset()
        {
            RemoveAllListeners(_menuButton);
        }
    }
}