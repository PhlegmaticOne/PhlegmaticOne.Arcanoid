using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Libs.Pooling.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using UnityEngine;

namespace Libs.Popups.Factory
{
    public class PopupFactory : IPopupFactory
    {
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly PopupSystemConfiguration _popupSystemConfiguration;
        private readonly RectTransform _mainCanvasTransform;

        public PopupFactory(IPoolProvider poolProvider, 
            RectTransform mainCanvasTransform,
            PopupSystemConfiguration popupSystemConfiguration)
        {
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _mainCanvasTransform = mainCanvasTransform;
            _popupSystemConfiguration = popupSystemConfiguration;
        }
        
        public T CreatePopup<T>() where T : Popup
        {
            var popup = _popupsPool.GetConcrete<T>();
            return (T)InitializePopup(popup);
        }

        public Popup CreatePopup(Popup prefab)
        {
            var popup = _popupsPool.GetByType(prefab.GetType());
            return InitializePopup(popup);
        }
        
        private Popup InitializePopup(Popup popup)
        {
            var popupConfiguration = _popupSystemConfiguration.FindConfigurationForPrefab(popup);
            Initialize(popup);
            popup.SetParentTransform(_mainCanvasTransform);
            popup.SetPopupConfiguration(popupConfiguration);
            return popup;
        }

        private void Initialize(Popup popup)
        {
            var type = popup.GetType();
            InjectConstructor(popup, type);
            InjectProperties(popup, type);
        }

        private void InjectProperties(Popup popup, Type type)
        {
            var propertiesToInject = type.GetProperties()
                .Where(x => x.GetCustomAttribute<PopupPropertyAttribute>() != null)
                .ToList();

            foreach (var propertyInfo in propertiesToInject)
            {
                InjectProperty(propertyInfo, popup);
            }
        }

        private void InjectConstructor(Popup popup, Type type)
        {
            var constructor = type.GetMethods()
                .SingleOrDefault(x => x.GetCustomAttribute<PopupConstructorAttribute>() != null);

            if (constructor == null)
            {
                return;
            }
            
            constructor.Invoke(popup, GetConstructorDependencies(constructor).ToArray());
        }

        private void InjectProperty(PropertyInfo propertyInfo, Popup popup)
        {
            var dependency = ServiceProviderAccessor.SearchService(propertyInfo.PropertyType);
            propertyInfo.SetValue(popup, dependency);
        }

        private List<object> GetConstructorDependencies(MethodInfo constructor)
        {
            var result = new List<object>();

            foreach (var dependency in constructor.GetParameters())
            {
                result.Add(ServiceProviderAccessor.SearchService(dependency.ParameterType));
            }

            return result;
        }
    }
}