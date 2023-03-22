using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Animations.Info;
using Libs.Popups.Controls;
using UnityEngine;

namespace Popups.Energy
{
    public class EnergyPopup : ViewModelPopup<EnergyPopupViewModel>, ILocalizable
    {
        [SerializeField] private ButtonControl _okControl;
        [SerializeField] private LocalizationBindableComponent _reasonText;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _showAnimationInfo;
        [SerializeField] private TweenAnimationInfo _closeAnimationInfo;
        
        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;

        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        protected override void SetupViewModel(EnergyPopupViewModel viewModel)
        {
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .FromLeft(_showAnimationInfo)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .ToRight(_closeAnimationInfo)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.OkControlAction, Animate.None());
            
            BindToAction(_okControl, viewModel.OkControlAction);
        }

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;
        
        public void ShowWithReasonPhraseKey(string reasonPhraseKey)
        {
            _reasonText.SetBindingData<string>(reasonPhraseKey);
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        public override void EnableInput() => _okControl.Enable();
        public override void DisableInput() => _okControl.Disable();

        public override void Reset()
        {
            ToZeroPosition();
            _localizationContext.Flush();
            _okControl.Reset();
            Unbind(ViewModel.OkControlAction);
        }
    }
}