using System.Collections.Generic;
using Common.Bag;
using Common.Energy;
using Common.Energy.Events;
using Common.Packs.Data.Models;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Controls;
using Popups.Common.Controls;
using Popups.Common.Elements;
using UnityEngine;

namespace Popups.MainGameMenu
{
    public class MainGameMenuPopup : ViewModelPopup<MainGameMenuViewModel>, ILocalizable
    {
        [SerializeField] private ResettableElement _mainPopupTransform;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private ButtonControl _continueControl;
        [SerializeField] private SpendEnergyControl _restartControl;
        [SerializeField] private EnergyView _energyView;

        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _fadeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _mainPopupAnimationInfo;
        [SerializeField] private float _energyAnimationTime;

        private LocalizationContext _localizationContext;
        private EnergyController _energyController;
        private EnergyManager _energyManager;
        private PackGameData _currentPackData;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager,
            IObjectBag objectBag,
            EnergyManager energyManager)
        {
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
            _energyManager = energyManager;
            _currentPackData = objectBag.Get<GameData>().PackGameData;
            _energyController = new EnergyController(energyManager, _energyView);
            _restartControl.SetEnergy(_currentPackData.PackConfiguration.StartLevelEnergy);
            _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
        }

        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_restartControl);
        }

        protected override void SetupViewModel(MainGameMenuViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(DefaultAnimations.FadeIn(PopupView.CanvasGroup, _fadeAnimationInfo));
                s.Append(DefaultAnimations.FromLeft(_mainPopupTransform.RectTransform, RectTransform, _mainPopupAnimationInfo));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(DefaultAnimations.ToRight(_mainPopupTransform.RectTransform, RectTransform, _mainPopupAnimationInfo));
                s.Append(DefaultAnimations.FadeOut(PopupView.CanvasGroup, _fadeAnimationInfo));
            }));
            SetAnimation(viewModel.RestartControlAction, new DoTweenSequenceAnimation(s =>
            {
                s.AppendCallback(() => _energyView
                    .ChangeEnergyAnimate(-_currentPackData.PackConfiguration.StartLevelEnergy, _energyAnimationTime));
                s.AppendInterval(_energyAnimationTime);
            }));
            SetAnimation(viewModel.BackControlAction, DefaultAnimations.None());
            SetAnimation(viewModel.ContinueControlAction, DefaultAnimations.None());
            
            BindToActionWithValue(_restartControl, viewModel.RestartControlAction, viewModel);
            BindToActionWithValue(_continueControl, viewModel.ContinueControlAction, viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        public override void EnableInput()
        {
            _backControl.Enable();
            _restartControl.Enable();
            _continueControl.Enable();
        }

        public override void DisableInput()
        {
            _backControl.Disable();
            _restartControl.Disable();
            _continueControl.Disable();
        }
        
        public override void Reset()
        {
            ToZeroPosition();
            _mainPopupTransform.Reset();
            _energyController.Disable();
            _localizationContext.Flush();
            _continueControl.Reset();
            _restartControl.Reset();
            _continueControl.Reset();
            _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.ContinueControlAction);
            Unbind(ViewModel.RestartControlAction);
        }
    }
}