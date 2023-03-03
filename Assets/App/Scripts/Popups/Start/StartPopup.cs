using Libs.Popups;
using Libs.Popups.Base;
using Popups.PackChoose;
using Popups.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Start
{
    public class StartPopup : Popup
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _startGameButton;

        private IPopupManager _popupManager;

        public void Initialize(IPopupManager popupManager)
        {
            _popupManager = popupManager;
            ConfigureSettingsButton();
            ConfigureStartGameButton();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_settingsButton);
            EnableBehaviour(_startGameButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_settingsButton);
            DisableBehaviour(_startGameButton);
        }

        public override void Reset()
        {
            RemoveAllListeners(_settingsButton);
            RemoveAllListeners(_startGameButton);
        }

        private void ConfigureSettingsButton()
        {
            _settingsButton.onClick.AddListener(() =>
            {
                _popupManager.SpawnPopup<SettingsPopup>();
            });
        }
        
        private void ConfigureStartGameButton()
        {
            _startGameButton.onClick.AddListener(() =>
            {
                _popupManager.HidePopup();
            });
        }

        protected override void OnHid()
        {
            _popupManager.SpawnPopup<PackChoosePopup>();
        }
    }
}