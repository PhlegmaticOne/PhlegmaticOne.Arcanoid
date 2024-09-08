using Libs.Localization.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Common.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private const string Key = "LocaleKey";
        
        public void SaveLocale(LocaleInfo localeInfo)
        {
            var json = JsonConvert.SerializeObject(localeInfo);
            PlayerPrefs.SetString(Key, json);
        }

        public LocaleInfo Saved
        {
            get
            {
                var json = PlayerPrefs.GetString(Key, string.Empty);
                
                if (json == string.Empty)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<LocaleInfo>(json);
            }
        }
    }
}