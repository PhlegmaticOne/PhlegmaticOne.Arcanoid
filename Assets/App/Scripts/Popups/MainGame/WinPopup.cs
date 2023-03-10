using System.Collections.Generic;
using Common.Configurations.Packs;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.MainGame.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class WinPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;
        [SerializeField] private PackageInfoView _packageInfoView;

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
        }

        public void UpdatePackInfoView(PackConfiguration packConfiguration)
        {
            _packageInfoView.SetPackInfo(packConfiguration);
        }

        public override void EnableInput()
        {
            EnableBehaviour(_nextLevelButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_nextLevelButton);
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
        }
    }
}