using Libs.Localization.Models;

namespace Common.Localization
{
    public interface ILocalizationProvider
    {
        void SaveLocale(LocaleInfo localeInfo);
        LocaleInfo Saved { get; }
    }
}