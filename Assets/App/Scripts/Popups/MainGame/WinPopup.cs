using System.Collections.Generic;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class WinPopup : Popup, ILocalizable
    {
        [SerializeField] private Button _nextLevelButton;
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