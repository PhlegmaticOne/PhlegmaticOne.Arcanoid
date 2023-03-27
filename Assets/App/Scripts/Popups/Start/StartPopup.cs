using DG.Tweening;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Start
{
    public class StartPopup : ViewModelPopup<StartPopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private StartPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private ButtonControl _settingsControl;
        [SerializeField] private ButtonControl _exitControl;
        [SerializeField] private ButtonControl _startGameControl;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.Refresh();
        }
        
        protected override void SetupViewModel(StartPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenSequenceAnimation(s =>
            {
                s.Append(Animate.CanvasGroup(PopupView.CanvasGroup)
                    .FadeIn(_animationConfiguration.FadeInAnimation));
                s.Append(Animate.RectTransform(_startGameControl.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ButtonsAppearAnimation));
                s.Append(Animate.RectTransform(_exitControl.RectTransform)
                    .RelativeTo(RectTransform)
                    .Appear(_animationConfiguration.ButtonsAppearAnimation));
            }));
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Disappear(_animationConfiguration.CloseAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.ExitControlAction, Animate.RectTransform(_exitControl.RectTransform)
                .RelativeTo(RectTransform)
                .ToRight(_animationConfiguration.ButtonsDisappearAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.PlayControlAction, Animate.RectTransform(_startGameControl.RectTransform)
                .RelativeTo(RectTransform)
                .ToRight(_animationConfiguration.ButtonsDisappearAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.SettingsControlAction, Animate.None());

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
            
            _localizationComponent.Unbind();
            _settingsControl.Reset();
            _startGameControl.Reset();
            _exitControl.Reset();
            
            Unbind(ViewModel.PlayControlAction);
            Unbind(ViewModel.ExitControlAction);
            Unbind(ViewModel.SettingsControlAction);
        }
    }
}