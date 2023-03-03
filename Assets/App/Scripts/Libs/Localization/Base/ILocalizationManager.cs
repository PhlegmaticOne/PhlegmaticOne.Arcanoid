using System.Collections.Generic;
using UnityEngine.Events;

namespace Libs.Localization.Base
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