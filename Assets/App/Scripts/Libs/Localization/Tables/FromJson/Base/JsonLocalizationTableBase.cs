using System;
using Libs.Localization.Base;
using Newtonsoft.Json;

namespace Libs.Localization.Tables.FromJson.Base
{
    public abstract class JsonLocalizationTableBase<TType, TValue> : ILocalizationTable
    {
        protected readonly TType DeserializedObject;
        
        protected JsonLocalizationTableBase(string json)
        {
            DeserializedObject = JsonConvert.DeserializeObject<TType>(json);
        }

        public object GetLocalizedValue(string key, Type valueType)
        {
            if (valueType == typeof(TValue))
            {
                return GetLocalizedValue(key);
            }

            return null;
        }
        
        protected abstract TValue GetLocalizedValue(string key);
    }
}