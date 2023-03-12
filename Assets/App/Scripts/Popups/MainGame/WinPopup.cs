using System;
using System.Collections.Generic;
using Common.Bag;
using Common.Configurations.Packs;
using Common.Data.Models;
using Game.Commands.Base;
using Game.ViewModels;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Popups.MainGame.Views;
using UnityEngine;
using UnityEngine.UI;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Popups.MainGame
{
    public class WinPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;
        [SerializeField] private PackageInfoView _packageInfoView;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;

        private IObjectBag _objectBag;
        private WinMenuViewModel _winMenuViewModel;
        private ICommand _onCloseCommand;
        private Action _onCloseAction;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            _bindableComponents.Add(_packageInfoView.PackNameLocalizationComponent);
            return _bindableComponents;
        }

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _objectBag = serviceProvider.GetRequiredService<IObjectBag>();
            ConfigureNextLevelButton();
        }

        public void SetupViewModel(WinMenuViewModel winMenuViewModel) => _winMenuViewModel = winMenuViewModel;

        public void OnShowing()
        {
            UpdatePackInfoView(_objectBag.Get<GameData>().PackConfiguration);
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            _winMenuViewModel.OnShowingCommand.Execute();
        }

        public void OnClose(Action action) => _onCloseAction = action;

        protected override void OnClosed()
        {
            _onCloseCommand?.Execute();
            _onCloseAction?.Invoke();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_nextLevelButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_nextLevelButton);
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            _onCloseAction = null;
            RemoveAllListeners(_nextLevelButton);
        }

        private void ConfigureNextLevelButton()
        {
            _nextLevelButton.onClick.AddListener(() =>
            {
                var pack = _objectBag.Get<GameData>().PackConfiguration;
                
                _onCloseCommand = pack.PassedLevelsCount == pack.LevelsCount - 1 ? 
                    _winMenuViewModel.OnLastClosedCommand :
                    _winMenuViewModel.OnClosedCommand;
                
                _winMenuViewModel.OnNextButtonClickCommand.Execute();
                
                PopupManager.CloseLastPopup();
            });
        }
        
        private void UpdatePackInfoView(PackConfiguration packConfiguration)
        {
            _packageInfoView.SetPackInfo(packConfiguration);
        }
    }
}