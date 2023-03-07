using System;

namespace Libs.Localization.Base
{
    public interface ILocalizationTable
    {
        object GetLocalizedValue(string key, Type valueType);
    }
}