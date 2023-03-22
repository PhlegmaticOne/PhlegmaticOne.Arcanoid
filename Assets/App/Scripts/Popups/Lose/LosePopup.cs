using System.Collections.Generic;
using Common.Energy;
using Common.Energy.Events;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Info;
using Libs.Popups.Controls;
using Popups.Common.Controls;
using Popups.Common.Elements;
using UnityEngine;

namespace Popups.Lose
{
    public class LosePopup : ViewModelPopup<LosePopupViewModel>, ILocalizable
    {
        [SerializeField] private ResettableElement _mainPopupElement;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private SpendEnergyControl _restartControl;
        [SerializeField] private SpendEnergyControl _buyLifeControl;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private LocalizationContext _localizationContext;
        private EnergyController _energyController;
        private EnergyManager _energyManager;

        [SerializeField] private TweenAnimationInfo _fadeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _mainPopupAnimationInfo;
        [SerializeField] private float _energyAnimationTime;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, EnergyManager energyManager)
        {
            _energyManager = energyManager;
            _energyController = new EnergyController(energyManager, _energyView);
            _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
            
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_restartControl);
            UpdateControl(_buyLifeControl);
        }

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;
        
        protected override void SetupViewModel(LosePopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeIn(_fadeAnimationInfo));
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .FromLeft(_mainPopupAnimationInfo));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .ToRight(_mainPopupAnimationInfo));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeOut(_fadeAnimationInfo));
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
            
            _localizationContext.Flush();
            _energyController.Disable();
            _backControl.Reset();
            _restartControl.Reset();
            _buyLifeControl.Reset();
            _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
            
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.BuyLifeControlAction);
            Unbind(ViewModel.RestartControlAction);
        }

        private void ShowEnergyInfo()
        {
            _restartControl.SetEnergy(GetStartLevelEnergy());
            _buyLifeControl.SetEnergy(GetContinueLevelEnergy());
        }

        private DoTweenSequenceAnimation GetOnCloseAnimation(int energyToSpend) =>
            new DoTweenSequenceAnimation(s =>
            {
                _energyView.AppendAnimationToSequence(s, energyToSpend, _energyAnimationTime);
            });

        private int GetStartLevelEnergy() => ViewModel.CurrentPack.PackConfiguration.StartLevelEnergy;
        private int GetContinueLevelEnergy() => ViewModel.CurrentPack.PackConfiguration.ContinueLevelEnergy;
    }
}