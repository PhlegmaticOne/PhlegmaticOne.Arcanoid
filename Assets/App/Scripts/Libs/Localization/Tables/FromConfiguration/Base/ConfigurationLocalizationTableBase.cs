using System;
using System.Collections.Generic;
using Libs.Localization.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Localization.Tables.FromConfiguration.Base
{
    public abstract class ConfigurationLocalizationTableBase : SerializedScriptableObject, ILocalizationTable
    {
        public abstract Type ValueType { get; }
        public abstract object GetLocalizedValue(string key, Type type);
    }
    
    public class ConfigurationLocalizationTableBase<T> : ConfigurationLocalizationTableBase
    {
        [SerializeField] private Dictionary<string, T> LocalizedValues;

        public override Type ValueType => typeof(T);
        public override object GetLocalizedValue(string key, Type type)
        {
            if (LocalizedValues.TryGetValue(key, out var value) && value.GetType() == type)
            {
                return value;
            }

            return null;
        }
    }
}