using Libs.Localization;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Energy
{
    public class EnergyPopup : ViewModelPopup<EnergyPopupViewModel>
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private ButtonControl _okControl;
        [SerializeField] private LocalizationBindableComponent _reasonText;
        [SerializeField] private EnergyPopupAnimationConfiguration _animationConfiguration;
        
        private ILocalizationManager _localizationManager;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        protected override void SetupViewModel(EnergyPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Appear(_animationConfiguration.ShowAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Disappear(_animationConfiguration.CloseAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.OkControlAction, Animate.None());
            
            BindToAction(_okControl, viewModel.OkControlAction);
        }

        public void ShowWithReasonPhraseKey(string reasonPhraseKey)
        {
            _reasonText.SetBindingData<string>(reasonPhraseKey);
            _localizationComponent.BindInitial(_localizationManager);
            _localizationComponent.Refresh();
        }

        public override void EnableInput() => _okControl.Enable();
        public override void DisableInput() => _okControl.Disable();

        public override void Reset()
        {
            ToZeroPosition();
            _localizationComponent.Unbind();
            _okControl.Reset();
            Unbind(ViewModel.OkControlAction);
        }
    }
}