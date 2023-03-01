using System.Collections.Generic;
using UnityEngine.Events;

namespace App.Scripts.Common.Localization.Base
{
    public interface ILocalizationManager
    {
        event UnityAction LocaleChanged;
        string CurrentLocale { get; }
        void SetLocale(string locale);
        IEnumerable<string> GetAvailableLocales();
        string GetLocalizedString(string key);
    }
}