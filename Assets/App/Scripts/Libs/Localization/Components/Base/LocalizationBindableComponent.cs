using System;
using Libs.Localization.Base;
using UnityEngine;

namespace Libs.Localization.Components.Base
{
    public abstract class LocalizationBindableComponent : MonoBehaviour, ILocalizationBindable
    {
        [SerializeField] private string _bindingKey;
        private Type _bindingType;

        public string BindingKey => _bindingKey;

        public virtual Type BindingType => _bindingType;

        public void SetBindingData<T>(string key)
        {
            _bindingKey = key;
            _bindingType = typeof(T);
        }
        
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