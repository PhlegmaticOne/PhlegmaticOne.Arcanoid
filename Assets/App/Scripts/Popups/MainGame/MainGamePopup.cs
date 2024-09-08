using Common.Game.Providers;
using Game.Logic.Systems.Health;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Controls;
using Popups.MainGame.Controls;
using Popups.MainGame.Views;
using UnityEngine;

namespace Popups.MainGame
{
    public class MainGamePopup : ViewModelPopup<MainGamePopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private ButtonControl _menuControl;
        [SerializeField] private WinControl _winControl;
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private LevelPassPercentageView _levelPassPercentageView;
        [SerializeField] private HealthBarView _healthBarView;

        public HealthBarView HealthBarView => _healthBarView;
        
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.Refresh();
        }
        
        protected override void SetupViewModel(MainGamePopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, Animate.None());
            SetAnimation(viewModel.CloseAction, Animate.None());
            SetAnimation(viewModel.MenuControlAction, Animate.None());
            SetAnimation(viewModel.WinControlAction, Animate.None());
            
            BindToAction(_menuControl, viewModel.MenuControlAction);
            BindToAction(_winControl, viewModel.WinControlAction);
        }
        
        public override void EnableInput()
        {
            _menuControl.Enable();
            _winControl.Enable();
        }

        public override void DisableInput()
        {
            _menuControl.Disable();
            _winControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _localizationComponent.Unbind();
            _menuControl.Reset();
            _winControl.Reset();
            
            Unbind(ViewModel.MenuControlAction);
            Unbind(ViewModel.WinControlAction);
        }
        
        public void InitializeHealthBar(int lifesCount)
        {
            _healthBarView.Clear();
            _healthBarView.Initialize(lifesCount);
        }
        
        public void UpdateHeader(GameData gameData)
        {
            _packageInfoView.SetPackInfo(gameData.PackGameData);
            _levelPassPercentageView.SetInNormalizedPercentageInstant(0);
        }

        public void UpdateLevelPassPercentageView(float normalizedPercentage) => 
            _levelPassPercentageView.SetInNormalizedPercentageAnimate(normalizedPercentage);
    }
}