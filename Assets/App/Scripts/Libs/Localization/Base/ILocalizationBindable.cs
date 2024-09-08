using System;

namespace Libs.Localization.Base
{
    public interface ILocalizationBindable
    {
        string BindingKey { get; }
        Type BindingType { get; }
        void SetLocalizedValue(object value);
    }
}