using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Settings
{
    public class SettingsPopup : ViewModelPopup<SettingsPopupViewModel>
    {
        [SerializeField] private SettingsPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private LocalizationSelector _localizationSelector;
        [SerializeField] private ButtonControl _closeControl;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationSelector.Initialize(localizationManager);
        }

        protected override void SetupViewModel(SettingsPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Appear(_animationConfiguration.ShowAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Disappear(_animationConfiguration.CloseAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseControlAction, Animate.None());
            
            BindToAction(_closeControl, viewModel.CloseControlAction);
        }

        public override void EnableInput()
        {
            _localizationSelector.Enable();
            _closeControl.Enable();
        }

        public override void DisableInput()
        {
            _localizationSelector.Disable();
            _closeControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _localizationSelector.Reset();
            _closeControl.Reset();
            Unbind(ViewModel.CloseControlAction);
        }
    }
}