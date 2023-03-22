using System.Collections.Generic;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Start
{
    public class StartPopup : ViewModelPopup<StartPopupViewModel>, ILocalizable
    {
        [SerializeField] private ButtonControl _settingsControl;
        [SerializeField] private ButtonControl _exitControl;
        [SerializeField] private ButtonControl _startGameControl;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _showAnimationInfo;
        [SerializeField] private TweenAnimationInfo _closeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _buttonAppearAnimationInfo;
        [SerializeField] private TweenAnimationInfo _buttonDisappearAnimationInfo;

        private LocalizationContext _localizationContext;
        
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void SetupViewModel(StartPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(DefaultAnimations
                    .FadeIn(PopupView.CanvasGroup, _showAnimationInfo));
                s.Append(DefaultAnimations
                    .FromRight(_startGameControl.RectTransform, RectTransform, _buttonAppearAnimationInfo));
                s.Append(DefaultAnimations
                    .FromRight(_exitControl.RectTransform, RectTransform, _buttonAppearAnimationInfo));
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToRight(RectTransform, ParentTransform, _closeAnimationInfo);
            }));
            SetAnimation(viewModel.ExitControlAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToRight(_exitControl.RectTransform, RectTransform, _buttonDisappearAnimationInfo);
            }));
            SetAnimation(viewModel.PlayControlAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToRight(_startGameControl.RectTransform, RectTransform, _buttonDisappearAnimationInfo);
            }));
            SetAnimation(viewModel.SettingsControlAction, DefaultAnimations.None());

            BindToActionWithValue(_startGameControl, viewModel.PlayControlAction, viewModel);
            BindToAction(_settingsControl, viewModel.SettingsControlAction);
            BindToAction(_exitControl, viewModel.ExitControlAction);
        }

        public override void EnableInput()
        {
            _settingsControl.Enable();
            _exitControl.Enable();
            _startGameControl.Enable();
        }

        public override void DisableInput()
        {
            _settingsControl.Disable();
            _exitControl.Disable();
            _startGameControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            
            _localizationContext.Flush();
            
            _settingsControl.Reset();
            _startGameControl.Reset();
            _exitControl.Reset();
            
            Unbind(ViewModel.PlayControlAction);
            Unbind(ViewModel.ExitControlAction);
            Unbind(ViewModel.SettingsControlAction);
        }
    }
}