using Abstracts.Popups;
using Abstracts.Popups.Base;
using App.Scripts.Common.Localization.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
{
    public class StartPopup : Popup
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _startGameButton;

        private IPopupManager _popupManager;
        private ILocalizationManager _localizationManager;

        public void Initialize(IPopupManager popupManager, ILocalizationManager localizationManager)
        {
            _popupManager = popupManager;
            _localizationManager = localizationManager;
            ConfigureSettingsButton();
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
                _popupManager.SpawnPopup<SettingsPopup>(popup =>
                {
                    popup.Initialize(_popupManager, _localizationManager);
                });
            });
        }
    }
}