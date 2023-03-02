using Abstracts.Popups;
using Abstracts.Popups.Base;
using Common.Localization;
using Common.Localization.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
{
    public class SettingsPopup : Popup
    {
        [SerializeField] private LocalizationSelector _localizationSelector;
        [SerializeField] private Button _closeButton;

        public void Initialize(IPopupManager popupManager, ILocalizationManager localizationManager)
        {
            _localizationSelector.Initialize(localizationManager);
            _closeButton.onClick.AddListener(popupManager.HidePopup);
        }

        public override void EnableInput() => _localizationSelector.Enable();
        public override void DisableInput() => _localizationSelector.Disable();
        public override void Reset()
        {
            _closeButton.onClick.RemoveAllListeners();
            _localizationSelector.Reset();
        }
    }
}