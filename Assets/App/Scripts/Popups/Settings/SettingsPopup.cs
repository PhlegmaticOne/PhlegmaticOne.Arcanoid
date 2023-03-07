using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Settings
{
    public class SettingsPopup : Popup
    {
        [SerializeField] private LocalizationSelector _localizationSelector;
        [SerializeField] private Button _closeButton;
        
        private ILocalizationManager _localizationManager;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            
            _localizationSelector.Initialize(_localizationManager);
            ConfigureCloseButton();
        }

        public override void EnableInput() => _localizationSelector.Enable();
        public override void DisableInput() => _localizationSelector.Disable();
        public override void Reset()
        {
            _localizationSelector.Reset();
            RemoveAllListeners(_closeButton);
        }
        
        private void ConfigureCloseButton()
        {
            _closeButton.onClick.AddListener(PopupManager.CloseLastPopup);
        }
    }
}