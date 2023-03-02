using Abstracts.Popups;
using Abstracts.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
{
    public class MainGamePopup : Popup
    {
        [SerializeField] private Button _menuButton;

        private IPopupManager _popupManager;

        public void Initialize(IPopupManager popupManager)
        {
            _popupManager = popupManager;
            ConfigureMenuButton();
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