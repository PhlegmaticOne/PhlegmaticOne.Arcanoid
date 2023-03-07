using System;
using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.PackChoose;
using Popups.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Start
{
    public class StartPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private IPopupManager _popupManager;
        private Action _spawnPopupAction;
        private ILocalizationManager _localizationManager;

        private LocalizationContext _localizationContext;

        public void Initialize(IPopupManager popupManager, ILocalizationManager localizationManager)
        {
            _popupManager = popupManager;
            _localizationManager = localizationManager;
            ConfigureSettingsButton();
            ConfigureStartGameButton();
        }

        protected override void OnShow()
        {
            _localizationContext = LocalizationContext.Create(_localizationManager);
            _localizationContext.BindLocalizable(this);
            _localizationContext.Refresh();
        }
        
        protected override void OnClosed()
        {
            _spawnPopupAction?.Invoke();
            _localizationContext.Flush();
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
                _spawnPopupAction = () => _popupManager.SpawnPopup<PackChoosePopup>();
                _popupManager.CloseLastPopup();
            });
        }

        

        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            return _bindableComponents;
        }
    }
}