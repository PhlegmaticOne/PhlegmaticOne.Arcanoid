using System;
using System.Collections.Generic;
using Libs.Localization.Models;

namespace Libs.Localization.Base
{
    public interface ILocalizationManager
    {
        event Action<LocaleInfo> LocaleChanged;
        LocaleInfo CurrentLocale { get; }
        void SetLocale(LocaleInfo locale);
        IEnumerable<LocaleInfo> GetAvailableLocales();
        object GetLocalizedValue(string key, Type valueType);
        T GetLocalizedValue<T>(string key);
    }
}