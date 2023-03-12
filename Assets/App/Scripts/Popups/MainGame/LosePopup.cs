using System.Collections.Generic;
using Game.ViewModels;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class LosePopup : Popup, ILocalizable
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;

        private LosePopupViewModel _losePopupViewModel;
        private UnityAction _onCloseAction;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            ConfigureRestartButton();
        }

        public void SetupViewModel(LosePopupViewModel losePopupViewModel) => _losePopupViewModel = losePopupViewModel;
        public void OnShowing() => _losePopupViewModel.OnShowingCommand.Execute();
        public void OnClose(UnityAction action) => _onCloseAction = action;

        public override void EnableInput()
        {
            EnableBehaviour(_restartButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_restartButton);
        }

        protected override void OnClosed()
        {
            _losePopupViewModel.RestartButtonCommand.Execute();
            _onCloseAction?.Invoke();
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            _onCloseAction = null;
            RemoveAllListeners(_restartButton);
        }

        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
    }
}