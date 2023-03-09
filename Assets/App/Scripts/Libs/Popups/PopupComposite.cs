using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Pooling;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using Libs.Popups.Animations;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using UnityEngine;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Libs.Popups
{
    public class PopupComposite : MonoBehaviour
    {
        [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;
        [SerializeField] private RectTransform _parent;
        public void AddPopupsToPool(PoolBuilder poolBuilder)
        {
            poolBuilder.AddAbstractPool(new UnityAbstractObjectPool<Popup>(CreatePopupPrefabInfos()));
        }
        
        public void AddPopupsToPool(PoolBuilder poolBuilder, RectTransform parent)
        {
            _parent = parent;
            poolBuilder.AddAbstractPool(new UnityAbstractObjectPool<Popup>(CreatePopupPrefabInfos()));
        }

        public IPopupManager CreatePopupManager(IPoolProvider poolProvider, Func<IServiceProvider> serviceProvider)
        {
            return new PopupManager(poolProvider, 
                new AppearanceAnimationsFactory(),
                new DisappearanceAnimationsFactory(),
                serviceProvider,
                _parent,
                _popupSystemConfiguration,
                _popupSystemConfiguration.StartFromSortingOrder);
        }

        private IEnumerable<PrefabInfo<Popup>> CreatePopupPrefabInfos() =>
            _popupSystemConfiguration.Popups.Select(x => new PrefabInfo<Popup>(x.Popup, _parent));
    }
}