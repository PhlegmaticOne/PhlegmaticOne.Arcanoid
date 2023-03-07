using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Models;

namespace Libs.Localization.Context
{
    public class LocalizationContext
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly List<ILocalizationBindable> _localizationBindables;
        private LocalizationContext(ILocalizationManager localizationManager)
        {
            _localizationBindables = new List<ILocalizationBindable>();
            _localizationManager = localizationManager;
            _localizationManager.LocaleChanged += LocalizationManagerOnLocaleChanged;
        }
        
        public static LocalizationContext Create(ILocalizationManager localizationManager) => 
            new LocalizationContext(localizationManager);
        

        public void BindLocalizable(ILocalizable localizable) => 
            _localizationBindables.AddRange(localizable.GetBindableComponents());

        public void Refresh() => 
            LocalizationManagerOnLocaleChanged(_localizationManager.CurrentLocale);

        public void Flush()
        {
            _localizationManager.LocaleChanged -= LocalizationManagerOnLocaleChanged;
            _localizationBindables.Clear();
        }

        private void LocalizationManagerOnLocaleChanged(LocaleInfo localeInfo)
        {
            foreach (var localizationBindable in _localizationBindables)
            {
                var localizedValue = _localizationManager
                    .GetLocalizedValue(localizationBindable.BindingKey, localizationBindable.BindingType);
                
                localizationBindable.SetLocalizedValue(localizedValue);
            }
        }
    }
}