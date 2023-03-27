using Common.Energy;
using Common.Energy.Events;
using DG.Tweening;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using Popups.Common.Controls;
using Popups.Common.Elements;
using UnityEngine;

namespace Popups.MainGameMenu
{
    public class MainGameMenuPopup : ViewModelPopup<MainGameMenuViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private MainGameMenuAnimationConfiguration _animationConfiguration;
        [SerializeField] private ResettableElement _mainPopupTransform;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private ButtonControl _continueControl;
        [SerializeField] private SpendEnergyControl _restartControl;
        [SerializeField] private EnergyView _energyView;

        private EnergyController _energyController;
        private EnergyManager _energyManager;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, EnergyManager energyManager)
        {
            _energyManager = energyManager;
            _energyController = new EnergyController(energyManager, _energyView);
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.Refresh();
            Subscribe();
        }
        
        protected override void SetupViewModel(MainGameMenuViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeIn(_animationConfiguration.FadeInAnimation));
                s.Append(Animate.RectTransform(_mainPopupTransform.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ShowAnimation));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupTransform.RectTransform)
                    .RelativeTo(RectTransform)
                    .Disappear(_animationConfiguration.CloseAnimation));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeOut(_animationConfiguration.FadeOutAnimation));
            }));
            SetAnimation(viewModel.RestartControlAction, new DoTweenSequenceAnimation(s =>
            {
                var energyToChange = -GetStartLevelEnergy();
                _energyView.AppendAnimationToSequence(s, energyToChange, _animationConfiguration.EnergyAnimationTime);
            }));
            SetAnimation(viewModel.BackControlAction, Animate.None());
            SetAnimation(viewModel.ContinueControlAction, Animate.None());
            
            BindToActionWithValue(_restartControl, viewModel.RestartControlAction, viewModel);
            BindToActionWithValue(_continueControl, viewModel.ContinueControlAction, viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
            
            _restartControl.SetEnergy(GetStartLevelEnergy());
        }
        
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
            _localizationComponent.Unbind();
            
            _continueControl.Reset();
            _restartControl.Reset();
            _backControl.Reset();
            
            Unsubscribe();
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.ContinueControlAction);
            Unbind(ViewModel.RestartControlAction);
        }
        
        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_restartControl);
        }
        
        private void Subscribe() => _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
        private void Unsubscribe() => _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;

        private int GetStartLevelEnergy() => ViewModel.CurrentPackGameData.PackConfiguration.StartLevelEnergy;
    }
}