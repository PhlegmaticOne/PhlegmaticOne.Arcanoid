using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Settings
{
    public class SettingsPopup : ViewModelPopup<SettingsPopupViewModel>
    {
        [SerializeField] private LocalizationSelector _localizationSelector;
        [SerializeField] private ButtonControl _closeControl;

        [SerializeField] private TweenAnimationInfo _showAnimationInfo;
        [SerializeField] private TweenAnimationInfo _closeAnimationInfo;
        
        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationSelector.Initialize(localizationManager);
        }

        protected override void SetupViewModel(SettingsPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.FromTop(RectTransform, ParentTransform, _showAnimationInfo);
            }));
            SetAnimation(viewModel.CloseAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToTop(RectTransform, ParentTransform, _closeAnimationInfo);
            }));
            SetAnimation(viewModel.CloseControlAction, DefaultAnimations.None());
            
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