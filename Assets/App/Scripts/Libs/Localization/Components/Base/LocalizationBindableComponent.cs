using System;
using Libs.Localization.Base;
using UnityEngine;

namespace Libs.Localization.Components.Base
{
    public abstract class LocalizationBindableComponent : MonoBehaviour, ILocalizationBindable
    {
        [SerializeField] private string _bindingKey;

        public string BindingKey => _bindingKey;

        public abstract Type BindingType { get; }
        
        public abstract void SetLocalizedValue(object value);
    }
    
    public abstract class LocalizationBindableComponent<T> : LocalizationBindableComponent, ILocalizationBindable
    {
        public override Type BindingType => typeof(T);
        
        public override void SetLocalizedValue(object value)
        {
            if (value is T generic)
            {
                SetLocalizedValue(generic);
            }
        }

        protected abstract void SetLocalizedValue(T value);
    }
}