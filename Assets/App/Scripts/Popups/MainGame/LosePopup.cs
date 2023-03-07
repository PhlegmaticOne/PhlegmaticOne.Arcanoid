using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class LosePopup : Popup, ILocalizable
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            ConfigureRestartButton();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_restartButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_restartButton);
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_restartButton);
        }

        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
    }
}