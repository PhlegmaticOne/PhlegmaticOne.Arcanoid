using Libs.Localization;
using Libs.Localization.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.ClearData
{
    public class ClearDataPopup : ViewModelPopup<ClearDataPopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private ClearDataPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private ButtonControl _acceptControl;
        [SerializeField] private ButtonControl _cancelControl;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.Refresh();
        }
        
        protected override void SetupViewModel(ClearDataPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .ScaleToOne(_animationConfiguration.ShowAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .ScaleToZero(_animationConfiguration.CloseAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.AcceptControlAction, Animate.None());
            SetAnimation(viewModel.CancelControlAction, Animate.None());
            
            BindToAction(_acceptControl, viewModel.AcceptControlAction);
            BindToAction(_cancelControl, viewModel.CancelControlAction);
        }
        
        public override void EnableInput()
        {
            _acceptControl.Enable();
            _cancelControl.Enable();
        }

        public override void DisableInput()
        {
            _acceptControl.Disable();
            _cancelControl.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _localizationComponent.Unbind();
            _acceptControl.Reset();
            _cancelControl.Reset();
        }
    }
}