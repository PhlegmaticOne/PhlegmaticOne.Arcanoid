using System.Collections.Generic;
using Common.Bag;
using Common.Packs.Data.Models;
using Game.Logic.Systems.Health;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Controls;
using Popups.MainGame.Views;
using UnityEngine;

namespace Popups.MainGame
{
    public class MainGamePopup : ViewModelPopup<MainGamePopupViewModel>, ILocalizable
    {
        [SerializeField] private ButtonControl _menuControl;
        [SerializeField] private ButtonControl _winControl;
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private LevelPassPercentageView _levelPassPercentageView;
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _showAnimationInfo;
        [SerializeField] private TweenAnimationInfo _closeAnimationInfo;
        private LocalizationContext _localizationContext;
        private IObjectBag _objectBag;

        public HealthBarView HealthBarView => _healthBarView;
            
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }
        
        protected override void SetupViewModel(MainGamePopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.FromBottom(RectTransform, ParentTransform, _showAnimationInfo);
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToBottom(RectTransform, ParentTransform, _closeAnimationInfo);
            }));
            SetAnimation(viewModel.MenuControlAction, DefaultAnimations.None());
            SetAnimation(viewModel.WinControlAction, DefaultAnimations.None());
            
            BindToAction(_menuControl, viewModel.MenuControlAction);
            BindToAction(_winControl, viewModel.WinControlAction);
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

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
            _localizationContext.Flush();
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
            UpdateLevelPassPercentageView(0);
        }

        public void UpdateLevelPassPercentageView(float normalizedPercentage)
        {
            _levelPassPercentageView.SetInNormalizedPercentage(normalizedPercentage);
        }
    }
}