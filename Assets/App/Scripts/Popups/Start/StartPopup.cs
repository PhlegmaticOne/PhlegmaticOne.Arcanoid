using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Popups.PackChoose;
using Popups.Settings;
using UnityEngine;
using UnityEngine.UI;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Popups.Start
{
    public class StartPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            ConfigureSettingsButton();
            ConfigureStartGameButton();
        }
        
        protected override void OnShowed()
        {
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
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
        
        protected override void OnClosed()
        {
            _localizationContext.Flush();
            base.OnClosed();
        }

        public override void Reset()
        {
            _localizationContext = null;
            RemoveAllListeners(_settingsButton);
            RemoveAllListeners(_startGameButton);
        }
        
        
        private void ConfigureSettingsButton()
        {
            _settingsButton.onClick.AddListener(() => PopupManager.SpawnPopup<SettingsPopup>());
        }

        private void ConfigureStartGameButton()
        {
            _startGameButton.onClick.AddListener(() =>
            {
                OnCloseSpawn<PackChoosePopup>();
                PopupManager.CloseLastPopup();
            });
        }
    }
}