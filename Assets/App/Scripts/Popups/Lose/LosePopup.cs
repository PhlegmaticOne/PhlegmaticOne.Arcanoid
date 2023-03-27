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

namespace Popups.Lose
{
    public class LosePopup : ViewModelPopup<LosePopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private LosePopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private ResettableElement _mainPopupElement;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private SpendEnergyControl _restartControl;
        [SerializeField] private SpendEnergyControl _buyLifeControl;
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
        
        protected override void SetupViewModel(LosePopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeIn(_animationConfiguration.FadeInAnimation));
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ShowAnimation));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .Disappear(_animationConfiguration.CloseAnimation));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeOut(_animationConfiguration.FadeOutAnimation));
            }));
            SetAnimation(viewModel.BuyLifeControlAction, GetOnCloseAnimation(-GetContinueLevelEnergy()));
            SetAnimation(viewModel.RestartControlAction, GetOnCloseAnimation(-GetStartLevelEnergy()));
            SetAnimation(viewModel.BackControlAction, Animate.None());
            
            BindToAction(_backControl, viewModel.BackControlAction);
            BindToActionWithValue(_buyLifeControl, viewModel.BuyLifeControlAction, viewModel);
            BindToActionWithValue(_restartControl, viewModel.RestartControlAction, viewModel);
            
            ShowEnergyInfo();
        }

        public override void EnableInput()
        {
            _restartControl.Enable();
            _buyLifeControl.Enable();
            _backControl.Enable();
        }

        public override void DisableInput()
        {
            _restartControl.Disable();
            _buyLifeControl.Disable();
            _backControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _mainPopupElement.Reset();
            
            _localizationComponent.Unbind();
            _energyController.Disable();
            _backControl.Reset();
            _restartControl.Reset();
            _buyLifeControl.Reset();
            Unsubscribe();
            
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.BuyLifeControlAction);
            Unbind(ViewModel.RestartControlAction);
        }
        
        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_restartControl);
            UpdateControl(_buyLifeControl);
        }

        private void ShowEnergyInfo()
        {
            _restartControl.SetEnergy(GetStartLevelEnergy());
            _buyLifeControl.SetEnergy(GetContinueLevelEnergy());
        }

        private DoTweenSequenceAnimation GetOnCloseAnimation(int energyToSpend) =>
            new DoTweenSequenceAnimation(s =>
            {
                _energyView.AppendAnimationToSequence(s, energyToSpend, _animationConfiguration.EnergyAnimationTime);
            });

        private void Subscribe() => _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
        private void Unsubscribe() => _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;

        private int GetStartLevelEnergy() => ViewModel.CurrentPack.PackConfiguration.StartLevelEnergy;
        private int GetContinueLevelEnergy() => ViewModel.CurrentPack.PackConfiguration.ContinueLevelEnergy;
    }
}