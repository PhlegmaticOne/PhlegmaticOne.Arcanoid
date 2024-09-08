using System.Collections.Generic;
using Common.Energy;
using Common.Energy.Events;
using Common.Packs.Data.Models;
using DG.Tweening;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using Popups.Common.Controls;
using Popups.Common.Elements;
using Popups.MainGame.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Win
{
    public class WinPopup : ViewModelPopup<WinPopupViewModel>, ILocalizable
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private WinPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private Image _lightsImage;
        [SerializeField] private TextMeshProUGUI _youPassedAllPacksText;
        [SerializeField] private ResettableElement _mainPopupElement;
        [SerializeField] private SpendEnergyControl _nextControl;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private EnergyView _energyView;

        private ILocalizationManager _localizationManager;
        private EnergyController _energyController;
        private EnergyManager _energyManager;
        private Tween _lightsTween;
        
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, EnergyManager energyManager)
        {
            _localizationManager = localizationManager;
            _energyManager = energyManager;
            _energyController = new EnergyController(energyManager, _energyView);
            Subscribe();
            SetupContinuousAnimations();
        }
        
        protected override void SetupViewModel(WinPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, CreateOnShowAnimation(viewModel));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .Disappear(_animationConfiguration.CloseAnimation));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup)
                    .FadeOut(_animationConfiguration.FadeOutAnimation));
            }));
            SetAnimation(viewModel.BackControlAction, Animate.None());
            
            BindNextControlAnimation(viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
            BindToActionWithValue(_nextControl, viewModel.NextControlAction, viewModel);
            
            UpdatePackInfoView();
            UpdateButtonsEnabled();
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            return new[] { _packageInfoView.PackNameLocalizationComponent };
        }

        public override void EnableInput()
        {
            _backControl.Enable();
            _nextControl.Enable();
        }

        public override void DisableInput()
        {
            _backControl.Disable();
            _nextControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _lightsTween.Kill();
            _youPassedAllPacksText.gameObject.SetActive(false);
            _lightsImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            _mainPopupElement.Reset();
            _localizationComponent.Unbind();
            _energyController.Disable();
            _nextControl.Reset();
            _backControl.Reset();
            Unsubscribe();
            Unbind(ViewModel.NextControlAction);
            Unbind(ViewModel.BackControlAction);
        }
        
        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_nextControl);
        }

        private void UpdatePackInfoView()
        {
            _packageInfoView.SetPackInfo(ViewModel.CurrentPackData);
            _localizationComponent.BindInitial(_localizationManager);
            _localizationComponent.AddNew(this);
            _localizationComponent.Refresh();
        }

        private void SetupContinuousAnimations()
        {
            _lightsTween = Animate.Transform(_lightsImage.transform)
                .FullCircleAnimate(_animationConfiguration.LightAnimation)
                .Play();
        }

        private IPopupAnimation CreateOnShowAnimation(WinPopupViewModel winPopupViewModel)
        {
            return new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup)
                    .FadeIn(_animationConfiguration.FadeInAnimation));
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ShowAnimation));
                _packageInfoView
                    .AppendShowAnimationToSequence(s, _animationConfiguration.PackViewScaleAnimation);
                s.AppendCallback(() => _packageInfoView.IncreaseLevel());
                _energyView
                    .AppendAnimationToSequence(s, GetWinEnergy(), _animationConfiguration.EnergyAnimationTime);
                
                if (winPopupViewModel.WinState == WinState.PackPassedFirstTime ||
                    winPopupViewModel.WinState == WinState.NextLevelInCurrentPack)
                {
                    s.AppendCallback(() => _nextControl.SetEnergy(GetStartNextLevelEnergy()));
                    s.Append(Animate.RectTransform(_nextControl.RectTransform)
                        .RelativeTo(RectTransform)
                        .Appear(_animationConfiguration.ButtonsAppearAnimation));
                }
                
                s.Append(Animate.RectTransform(_backControl.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ButtonsAppearAnimation));

                if (winPopupViewModel.WinState == WinState.AllPacksPassed)
                {
                    _youPassedAllPacksText.gameObject.SetActive(true);
                    s.Append(Animate.Transform(_youPassedAllPacksText.transform)
                        .GrowFromZeroX(_animationConfiguration.YouPassedAllPacksTextAppearAnimation));
                    return;
                }

                if (winPopupViewModel.WinState == WinState.PackPassedFirstTime)
                {
                    var newPackName = _localizationManager
                        .GetLocalizedValue<string>(winPopupViewModel.NextPackData.PackConfiguration.Name);
                    _packageInfoView.UpdatePackDataAnimate(GetNextPackGameData(), newPackName, 
                        s, _animationConfiguration.PackViewScaleAnimation, 
                        _animationConfiguration.ChangePackColorAnimation);
                }
            });
        }

        private void BindNextControlAnimation(WinPopupViewModel winPopupViewModel)
        {
            var startEnergy = GetStartNextLevelEnergy();
            var resultAnimation = startEnergy == -1 ? Animate.None() :
                new DoTweenSequenceAnimation(s =>
                {
                    _energyView.AppendAnimationToSequence(s, -startEnergy, _animationConfiguration.EnergyAnimationTime);
                });

            SetAnimation(winPopupViewModel.NextControlAction, resultAnimation);
        }

        private void UpdateButtonsEnabled()
        {
            if (ViewModel.WinState == WinState.AllPacksPassed ||
                ViewModel.WinState == WinState.PackPassedMultipleTime)
            {
                var mean = (_nextControl.RectTransform.localPosition + _backControl.RectTransform.localPosition) / 2f;
                _nextControl.SetActive(false);
                _backControl.RectTransform.localPosition = mean;
            }
            else
            {
                _nextControl.SetActive(true);
            }
        }

        private int GetStartNextLevelEnergy()
        {
            var winState = ViewModel.WinState;

            var packData = winState switch
            {
                WinState.NextLevelInCurrentPack => ViewModel.CurrentPackData,
                WinState.PackPassedFirstTime => ViewModel.NextPackData,
                _ => null
            };
            
            return packData == null ? -1 : packData.PackConfiguration.StartLevelEnergy;
        }

        private PackGameData GetNextPackGameData() => ViewModel.NextPackData;
        private int GetWinEnergy() => ViewModel.CurrentPackData.PackConfiguration.WinLevelEnergy;
        private void Subscribe() => _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
        private void Unsubscribe() => _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
    }
}