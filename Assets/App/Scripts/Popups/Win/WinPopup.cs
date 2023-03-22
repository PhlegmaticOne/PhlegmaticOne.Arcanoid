using System.Collections.Generic;
using Common.Energy;
using Common.Energy.Events;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;
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
        private static readonly Vector3 FullCircle = new Vector3(0, 0, 360);
        [SerializeField] private Image _lightsImage;
        [SerializeField] private TextMeshProUGUI _youPassedAllPacksText;
        [SerializeField] private ResettableElement _mainPopupElement;
        [SerializeField] private SpendEnergyControl _nextControl;
        [SerializeField] private ButtonControl _backControl;
        
        [SerializeField] private PackageInfoView _packageInfoView;
        [SerializeField] private TextMeshProLocalizationComponent _packNameText;
        [SerializeField] private Image _packImage;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _fadeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _mainPopupAnimationInfo;
        [SerializeField] private TweenAnimationInfo _nextControlAnimationInfo;
        [SerializeField] private float _energyAnimationTime;
        [SerializeField] private float _lightsCircleAnimationTime;
        [SerializeField] private float _scaleAnimationDuration;

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

        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel _)
        {
            UpdateControl(_nextControl);
        }

        protected override void SetupViewModel(WinPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, CreateOnShowAnimation(viewModel));
            SetAnimation(viewModel.CloseAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(DefaultAnimations
                    .ToRight(_mainPopupElement.RectTransform, RectTransform, _mainPopupAnimationInfo));
                s.Append(DefaultAnimations
                    .FadeOut(PopupView.CanvasGroup, _fadeAnimationInfo));
            }));
            SetAnimation(viewModel.BackControlAction, DefaultAnimations.None());
            
            BindNextControlAnimation(viewModel);
            BindToAction(_backControl, viewModel.BackControlAction);
            BindToActionWithValue(_nextControl, viewModel.NextControlAction, viewModel);
            UpdatePackInfoView();
            _nextControl.ChangeEnergyViewEnabled(viewModel.WinState != WinState.AllPacksPassed);
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
            _lightsTween = _lightsImage.transform
                .DORotate(FullCircle, _lightsCircleAnimationTime, RotateMode.FastBeyond360)
                .SetUpdate(true)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        private IPopupAnimation CreateOnShowAnimation(WinPopupViewModel winPopupViewModel)
        {
            return new DoTweenSequenceAnimation(s =>
            {
                var winEnergy = winPopupViewModel.CurrentPackData.PackConfiguration.WinLevelEnergy;
                _packNameText.transform.localScale = new Vector3(0, 1, 1);
                _packImage.transform.localScale = Vector3.zero;

                s.Append(DefaultAnimations
                    .FadeIn(PopupView.CanvasGroup, _fadeAnimationInfo));
                s.Append(DefaultAnimations
                    .FromLeft(_mainPopupElement.RectTransform, RectTransform, _mainPopupAnimationInfo));

                s.Append(_packImage.transform.DOScale(Vector3.one, _scaleAnimationDuration));
                s.Append(_packNameText.transform.DOScaleX(1, _scaleAnimationDuration));
                s.AppendCallback(() => _packageInfoView.IncreaseLevel());
                s.AppendCallback(() => _energyView.ChangeEnergyAnimate(winEnergy, _energyAnimationTime));
                s.AppendInterval(_energyAnimationTime);
                s.Append(DefaultAnimations
                    .FromLeft(_nextControl.RectTransform, RectTransform, _nextControlAnimationInfo));
                
                if (winPopupViewModel.WinState == WinState.AllPacksPassed)
                {
                    _youPassedAllPacksText.gameObject.SetActive(true);
                    _youPassedAllPacksText.transform.localScale = new Vector3(0, 1, 1);
                    s.Append(_youPassedAllPacksText.transform.DOScaleX(1, _scaleAnimationDuration));
                    return;
                }

                if (winPopupViewModel.WinState == WinState.PackPassedFirstTime)
                {
                    var nextPack = winPopupViewModel.NextPackData;
                    var nextPackConfiguration = nextPack.PackConfiguration;
                    var nextPackPersistentData = nextPack.PackPersistentData;
                    s.Append(_packImage.transform.DOScale(Vector3.zero, _scaleAnimationDuration));
                    s.Append(_packNameText.transform.DOScale(Vector3.zero, _scaleAnimationDuration));
                    s.AppendCallback(() =>
                    {
                        var sprite = nextPackConfiguration.PackImage;
                        _packImage.sprite = sprite;
                        _packNameText.SetBindingData<string>(nextPackConfiguration.Name);
                        _localizationContext.Refresh();
                        _packageInfoView.UpdateLevels(nextPackPersistentData);
                    });
                    s.Append(_packImage.transform.DOScale(Vector3.one, _scaleAnimationDuration));
                    s.Append(_packNameText.transform.DOScale(Vector3.one, _scaleAnimationDuration));
                }

                s.AppendCallback(() =>
                {
                    var configuration = winPopupViewModel.WinState == WinState.PackPassedFirstTime
                        ? winPopupViewModel.NextPackData.PackConfiguration
                        : winPopupViewModel.CurrentPackData.PackConfiguration;

                    _nextControl.SetEnergy(configuration.StartLevelEnergy);
                });
            });
        }

        private void BindNextControlAnimation(WinPopupViewModel winPopupViewModel)
        {
            var winState = winPopupViewModel.WinState;

            var packData = winState switch
            {
                WinState.NextLevelInCurrentPack => winPopupViewModel.CurrentPackData,
                WinState.PackPassedFirstTime => winPopupViewModel.NextPackData,
                _ => null
            };
            var startEnergy = packData == null ? 0 : packData.PackConfiguration.StartLevelEnergy;

            if (startEnergy == 0)
            {
                SetAnimation(winPopupViewModel.NextControlAction, DefaultAnimations.None());
                return;
            }
            
            SetAnimation(winPopupViewModel.NextControlAction, new DoTweenSequenceAnimation(s =>
            {
                s.AppendCallback(() => _energyView.ChangeEnergyAnimate(-startEnergy, _energyAnimationTime));
                s.AppendInterval(_energyAnimationTime);
            }));
        }
    }
}