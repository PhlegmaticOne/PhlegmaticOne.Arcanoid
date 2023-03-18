using System.Collections.Generic;
using Common.Bag;
using Common.Packs.Data.Models;
using Game.PopupRequires.ViewModels;
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
        [SerializeField] private HealthBarView _healthBarView;

        private LocalizationContext _localizationContext;
        private ILocalizationManager _localizationManager;
        private IObjectBag _objectBag;

        private MainGameViewModel _mainGameViewModel;

        public HealthBarView HealthBarView => _healthBarView;

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _objectBag = serviceProvider.GetRequiredService<IObjectBag>();
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
            InitializeHealthBar();
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_menuButton);
        }

        public void UpdateHeader()
        {
            var gameData = _objectBag.Get<GameData>();
            UpdatePackInfoView(gameData.PackGameData);
            UpdateLevelPassPercentageView(0);
        }

        public void UpdatePackInfoView(PackGameData packGameData)
        {
            _packageInfoView.SetPackInfo(packGameData);
        }
        
        public void UpdateLevelPassPercentageView(float normalizedPercentage)
        {
            _levelPassPercentageView.SetInNormalizedPercentage(normalizedPercentage);
        }

        public void InitializeHealthBar()
        {
            _healthBarView.Clear();
            _healthBarView.Initialize(_objectBag.Get<LevelData>().LifesCount);
        }

        private void ConfigureMenuButton()
        {
            _menuButton.onClick.AddListener(() =>
            {
                _mainGameViewModel.PauseCommand.Execute();
                var menuPopup = PopupManager.SpawnPopup<MainGameMenuPopup>();
                menuPopup.SetupViewModel(_mainGameViewModel.MainMenuViewModel);
                menuPopup.OnRestartSubmit(() => UpdateLevelPassPercentageView(0));
            });
        }
    }
}