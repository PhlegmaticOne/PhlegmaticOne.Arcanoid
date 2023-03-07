using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Localization.Base;
using Libs.Localization.Configurations;
using Libs.Localization.Installers.Base;
using Libs.Localization.Models;

namespace Libs.Localization.Implementations
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly Dictionary<LocaleConfiguration, LocaleTableCollection> _localeTables;

        private LocaleTableCollection _currentTableCollection;
        private LocaleInfo _currentLocale;
        
        public LocalizationManager(LocalizationSystemConfiguration localizationSystemConfiguration)
        {
            _localeTables = new Dictionary<LocaleConfiguration, LocaleTableCollection>();
            InitializeLocaleTables(localizationSystemConfiguration);
            SetLocale(localizationSystemConfiguration.DefaultLocale);
        }
        
        public event Action<LocaleInfo> LocaleChanged;
        
        public LocaleInfo CurrentLocale => _currentLocale;
        
        public void SetLocale(LocaleInfo locale)
        {
            var key = _localeTables.Keys.First(x => x.SystemName == locale.SystemName);
            SetLocale(key);
            LocaleChanged?.Invoke(CurrentLocale);
        }

        public IEnumerable<LocaleInfo> GetAvailableLocales() => _localeTables.Keys.Select(CreateLocaleInfo);

        public object GetLocalizedValue(string key, Type valueType) => _currentTableCollection.FindValue(key, valueType);
        
        public T GetLocalizedValue<T>(string key) => (T)GetLocalizedValue(key, typeof(T));


        private void InitializeLocaleTables(LocalizationSystemConfiguration localizationSystemConfiguration)
        {
            foreach (var localization in localizationSystemConfiguration.Localizations)
            {
                var table = new LocaleTableCollection(localization.Value);
                _localeTables.Add(localization.Key, table);
            }
        }

        private void SetLocale(LocaleConfiguration localeConfiguration)
        {
            _currentTableCollection = _localeTables[localeConfiguration];
            _currentLocale = CreateLocaleInfo(localeConfiguration);
        }

        private LocaleInfo CreateLocaleInfo(LocaleConfiguration localeConfiguration)
        {
            var displayName = GetLocalizedValue<string>(localeConfiguration.SystemName);
            return new LocaleInfo(localeConfiguration.SystemName, displayName);
        }
    }
    
    internal class LocaleTableCollection
    {
        private readonly List<ILocalizationTable> _localizationTables;
        
        public LocaleTableCollection(List<LocalizationTableInstallerBase> localizationTables)
        {
            _localizationTables = localizationTables.Select(x => x.CreateLocalizationTable()).ToList();
        }


        public object FindValue(string key, Type valueType)
        {
            foreach (var localizationTableBase in _localizationTables)
            {
                var value = localizationTableBase.GetLocalizedValue(key, valueType);
                
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}