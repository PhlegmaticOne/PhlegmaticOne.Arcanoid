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

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, EnergyManager energyManager)
        {
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
            _energyManager = energyManager;
            _energyController = new EnergyController(energyManager, _energyView);
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
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeIn(_fadeAnimationInfo));
                s.Append(Animate.RectTransform(_mainPopupTransform.RectTransform)
                    .RelativeTo(RectTransform)
                    .FromLeft(_mainPopupAnimationInfo));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupTransform.RectTransform)
                    .RelativeTo(RectTransform)
                    .ToRight(_mainPopupAnimationInfo));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeOut(_fadeAnimationInfo));
            }));
            SetAnimation(viewModel.RestartControlAction, new DoTweenSequenceAnimation(s =>
            {
                var energyToChange = -GetStartLevelEnergy();
                _energyView.AppendAnimationToSequence(s, energyToChange, _energyAnimationTime);
            }));
            SetAnimation(viewModel.BackControlAction, Animate.None());
            SetAnimation(viewModel.ContinueControlAction, Animate.None());
            
            BindToActionWithValue(_restartControl, viewModel.RestartControlAction, viewModel);
            BindToActionWithValue(_continueControl, viewModel.ContinueControlAction, viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
            
            _restartControl.SetEnergy(GetStartLevelEnergy());
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

        private int GetStartLevelEnergy() => ViewModel.CurrentPackGameData.PackConfiguration.StartLevelEnergy;
    }
}