using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.PackChoose;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGameMenuPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;
        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            ConfigureRestartButton();
            ConfigureContinueButton();
            ConfigureBackButton();
            
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_backButton);
            EnableBehaviour(_restartButton);
            EnableBehaviour(_continueButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_backButton);
            DisableBehaviour(_restartButton);
            DisableBehaviour(_continueButton);
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_backButton);
            RemoveAllListeners(_restartButton);
            RemoveAllListeners(_continueButton);
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                PopupManager.CloseAllPopupsInstant();
                PopupManager.SpawnPopup<PackChoosePopup>();
            });
        }
        
        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
        
        private void ConfigureContinueButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }

        
    }
}