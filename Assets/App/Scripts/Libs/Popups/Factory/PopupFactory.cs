using System;
using Libs.Pooling.Base;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Types;
using Libs.Popups.Configurations;
using UnityEngine;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Libs.Popups.Factory
{
    public class PopupFactory : IPopupFactory
    {
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly IPopupAnimationsFactory<AppearAnimationType> _appearAnimationsFactory;
        private readonly IPopupAnimationsFactory<DisappearAnimationType> _disappearAnimationsFactory;
        private readonly Func<IServiceProvider> _serviceProvider;
        private readonly PopupSystemConfiguration _popupSystemConfiguration;
        private readonly RectTransform _mainCanvasTransform;

        public PopupFactory(IPoolProvider poolProvider, 
            IPopupAnimationsFactory<AppearAnimationType> appearAnimationsFactory,
            IPopupAnimationsFactory<DisappearAnimationType> disappearAnimationsFactory,
            Func<IServiceProvider> serviceProvider,
            RectTransform mainCanvasTransform,
            PopupSystemConfiguration popupSystemConfiguration)
        {
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _appearAnimationsFactory = appearAnimationsFactory;
            _disappearAnimationsFactory = disappearAnimationsFactory;
            _serviceProvider = serviceProvider;
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
            var serviceProvider = _serviceProvider();
            popup.Initialize(serviceProvider);
            popup.SetParentTransform(_mainCanvasTransform);
            popup.SetAnimationFactories(_appearAnimationsFactory, _disappearAnimationsFactory);
            popup.SetPopupConfiguration(popupConfiguration);
            return popup;
        }
    }
}