using System.Collections.Generic;
using Common.Configurations.Packs;
using Common.Data.Models;
using Game.Accessors;
using Game.ViewModels;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.MainGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGamePopup : Popup, ILocalizable
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private LevelPassPercentageView _levelPassPercentageView;

        private LocalizationContext _localizationContext;
        private ILocalizationManager _localizationManager;
        private IObjectAccessor<GameData> _gameDataAccessor;

        private MainGameViewModel _mainGameViewModel;

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _gameDataAccessor = serviceProvider.GetRequiredService<IObjectAccessor<GameData>>();
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            ConfigureMenuButton();
        }

        public void SetupViewModel(MainGameViewModel mainGameViewModel) => _mainGameViewModel = mainGameViewModel;

        public override void EnableInput() => EnableBehaviour(_menuButton);

        public override void DisableInput() => DisableBehaviour(_menuButton);

        protected override void OnShowed()
        {
            UpdateHeader();
            _mainGameViewModel.StartCommand.Execute();
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_menuButton);
        }

        public void UpdateHeader()
        {
            UpdatePackInfoView(_gameDataAccessor.Get().PackConfiguration);
            UpdateLevelPassPercentageView(0);
        }

        public void UpdatePackInfoView(PackConfiguration packConfiguration)
        {
            _packageInfoView.SetPackInfo(packConfiguration);
        }
        
        public void UpdateLevelPassPercentageView(float normalizedPercentage)
        {
            _levelPassPercentageView.SetInNormalizedPercentage(normalizedPercentage);
        }

        private void ConfigureMenuButton()
        {
            _menuButton.onClick.AddListener(() =>
            {
                _mainGameViewModel.PauseCommand.Execute();
                var menuPopup = PopupManager.SpawnPopup<MainGameMenuPopup>();
                menuPopup.SetupViewModel(_mainGameViewModel.MainMenuViewModel);
            });
        }
    }
}