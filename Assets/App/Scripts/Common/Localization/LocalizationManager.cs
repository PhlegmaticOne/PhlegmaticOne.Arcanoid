using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Localization.Base;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Common.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly List<string> _tablesAvailable;
        public event UnityAction LocaleChanged;

        public LocalizationManager(IEnumerable<string> tableNames) => _tablesAvailable = tableNames.ToList();

        public IEnumerator Initialize()
        {
            yield return LocalizationSettings.InitializationOperation;
        }

        public string CurrentLocale => LocaleName(LocalizationSettings.SelectedLocale);

        public void SetLocale(string locale)
        {
            var availableLocale = LocalizationSettings.AvailableLocales.Locales.First(x => LocaleName(x) == locale);
            
            if (availableLocale != null)
            {
                LocalizationSettings.SelectedLocale = availableLocale;
                OnLocaleChanged();
            }
        }

        public IEnumerable<string> GetAvailableLocales() => 
            LocalizationSettings.AvailableLocales.Locales.Select(LocaleName);

        public string GetLocalizedString(string key)
        {
            foreach (var tableName in _tablesAvailable)
            {
                var value = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, key);
                
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    return value;
                }
            }

            return string.Empty;
        }

        private void OnLocaleChanged() => LocaleChanged?.Invoke();
        
        private static string LocaleName(Locale locale) => locale.Identifier.CultureInfo.DisplayName;
    }
}