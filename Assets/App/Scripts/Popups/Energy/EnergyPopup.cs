using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Energy
{
    public class EnergyPopup : Popup, ILocalizable
    {
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;
        [SerializeField] private LocalizationBindableComponent _reasonText;
        [SerializeField] private Button _okButton;
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            ConfigureOkButton();
        }

        public void ShowWithReasonPhraseKey(string reasonPhraseKey)
        {
            _reasonText.SetBindingData<string>(reasonPhraseKey);
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        public override void EnableInput() => EnableBehaviour(_okButton);

        public override void DisableInput() => DisableBehaviour(_okButton);

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_okButton);
        }

        private void ConfigureOkButton()
        {
            _okButton.onClick.AddListener(() =>
            {
                PopupManager.CloseLastPopup();
            });
        }
    }
}