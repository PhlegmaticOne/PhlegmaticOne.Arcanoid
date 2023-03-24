using System.Collections.Generic;
using Common.Energy;
using Common.Energy.Events;
using Common.Packs.Data.Models;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Info;
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
        [SerializeField] private Image _lightsImage;
        [SerializeField] private TextMeshProUGUI _youPassedAllPacksText;
        [SerializeField] private ResettableElement _mainPopupElement;
        [SerializeField] private SpendEnergyControl _nextControl;
        [SerializeField] private ButtonControl _backControl;
        
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _fadeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _mainPopupAnimationInfo;
        [SerializeField] private TweenAnimationInfo _nextControlAnimationInfo;
        [SerializeField] private TweenAnimationInfo _scaleAnimationInfo;
        [SerializeField] private TweenAnimationInfo _colorAnimationInfo;
        [SerializeField] private float _energyAnimationTime;
        [SerializeField] private float _lightsCircleAnimationTime;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        private EnergyController _energyController;
        private EnergyManager _energyManager;

        private Tween _lightsTween;
        
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, EnergyManager energyManager)
        {
            _localizationManager = localizationManager;
            _energyManager = energyManager;
            _energyController = new EnergyController(energyManager, _energyView);
            _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
            SetupContinuousAnimations();
        }
        
        protected override void SetupViewModel(WinPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, CreateOnShowAnimation(viewModel));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .ToRight(_mainPopupAnimationInfo));
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeOut(_fadeAnimationInfo));
            }));
            SetAnimation(viewModel.BackControlAction, Animate.None());
            
            BindNextControlAnimation(viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
            BindToActionWithValue(_nextControl, viewModel.NextControlAction, viewModel);
            
            UpdatePackInfoView();
            _nextControl.ChangeEnergyViewEnabled(viewModel.WinState != WinState.AllPacksPassed && 
                                                 viewModel.WinState != WinState.PackPassedMultipleTime);
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            _bindableComponents.Add(_packageInfoView.PackNameLocalizationComponent);
            return _bindableComponents;
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
            _localizationContext.Flush();
            _energyController.Disable();
            _nextControl.Reset();
            _backControl.Reset();
            _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
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
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        private void SetupContinuousAnimations()
        {
            _lightsTween = Animate.Transform(_lightsImage.transform)
                .FullCircleAnimate(_lightsCircleAnimationTime)
                .Play();
        }

        private IPopupAnimation CreateOnShowAnimation(WinPopupViewModel winPopupViewModel)
        {
            return new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup).FadeIn(_fadeAnimationInfo));
                s.Append(Animate.RectTransform(_mainPopupElement.RectTransform)
                    .RelativeTo(RectTransform)
                    .FromLeft(_mainPopupAnimationInfo));
                _packageInfoView.AppendShowAnimationToSequence(s, _scaleAnimationInfo);
                s.AppendCallback(() => _packageInfoView.IncreaseLevel());
                _energyView.AppendAnimationToSequence(s, GetWinEnergy(), _energyAnimationTime);
                s.Append(Animate.RectTransform(_nextControl.RectTransform)
                    .RelativeTo(RectTransform)
                    .FromLeft(_nextControlAnimationInfo));
                
                if (winPopupViewModel.WinState == WinState.AllPacksPassed)
                {
                    _youPassedAllPacksText.gameObject.SetActive(true);
                    s.Append(Animate.Transform(_youPassedAllPacksText.transform).GrowFromZeroX(_scaleAnimationInfo));
                    return;
                }

                if (winPopupViewModel.WinState == WinState.PackPassedFirstTime)
                {
                    _packageInfoView.UpdatePackDataAnimate(GetNextPackGameData(), s,
                        _scaleAnimationInfo, _colorAnimationInfo, () => _localizationContext.Refresh());
                }
                

                s.AppendCallback(() => _nextControl.SetEnergy(GetStartNextLevelEnergy()));
            });
        }

        private void BindNextControlAnimation(WinPopupViewModel winPopupViewModel)
        {
            var startEnergy = GetStartNextLevelEnergy();
            var resultAnimation = startEnergy == -1 ? Animate.None() :
                new DoTweenSequenceAnimation(s =>
                {
                    _energyView.AppendAnimationToSequence(s, -startEnergy, _energyAnimationTime);
                });

            SetAnimation(winPopupViewModel.NextControlAction, resultAnimation);
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
    }
}