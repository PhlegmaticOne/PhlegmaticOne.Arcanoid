using Common.WinButton;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using Popups.Settings.Controls;
using UnityEngine;

namespace Popups.Settings
{
    public class SettingsPopup : ViewModelPopup<SettingsPopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private SettingsPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private ButtonControl _closeControl;
        [SerializeField] private ButtonControl _clearDataControl;
        [SerializeField] private ToggleControl _winButtonEnabledControl;
        [SerializeField] private LocalizationDropdownControl _localizationDropdownControl;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager, IWinButtonEnabledProvider winButtonEnabledProvider)
        {
            _localizationDropdownControl.Initialize(localizationManager);
            _winButtonEnabledControl.Initialize(winButtonEnabledProvider.IsEnabled);
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.Refresh();
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
            SetAnimation(viewModel.ClearDataControlAction, Animate.None());
            SetAnimation(viewModel.WinButtonEnabledControlAction, Animate.None());
            SetAnimation(viewModel.ChangeLocaleControlAction, Animate.None());
            
            BindToAction(_closeControl, viewModel.CloseControlAction);
            BindToAction(_clearDataControl, viewModel.ClearDataControlAction);
            BindToAction(_winButtonEnabledControl, viewModel.WinButtonEnabledControlAction, false);
            BindToAction(_localizationDropdownControl, viewModel.ChangeLocaleControlAction, false, false);
        }

        public override void EnableInput()
        {
            _clearDataControl.Enable();
            _winButtonEnabledControl.Enable();
            _localizationDropdownControl.Enable();
            _closeControl.Enable();
        }

        public override void DisableInput()
        {
            _clearDataControl.Disable();
            _winButtonEnabledControl.Disable();
            _localizationDropdownControl.Disable();
            _closeControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _localizationComponent.Unbind();
            _localizationDropdownControl.Reset();
            _closeControl.Reset();
            _clearDataControl.Reset();
            _winButtonEnabledControl.Reset();
            Unbind(ViewModel.CloseControlAction);
        }
    }
}