using System.Collections.Generic;
using Game.Commands.Base;
using Game.ViewModels;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.PackChoose;
using UnityEngine;
using UnityEngine.Events;
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

        private MainMenuViewModel _mainMenuViewModel;
        private ICommand _onCloseCommand;

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

        public void SetupViewModel(MainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
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

        protected override void OnClosed() => _onCloseCommand.Execute();

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
                _onCloseCommand = _mainMenuViewModel.BackToPackMenuCommand;
                //_onBack?.Invoke();
                PopupManager.CloseAllPopupsInstant();
            });
        }
        
        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() =>
            {
                _onCloseCommand = _mainMenuViewModel.RestartCommand;
                PopupManager.CloseLastPopup();
            });
        }
        
        private void ConfigureContinueButton()
        {
            _continueButton.onClick.AddListener(() =>
            {
                _onCloseCommand = _mainMenuViewModel.ContinueCommand;
                PopupManager.CloseLastPopup();
            });
        }
    }
}