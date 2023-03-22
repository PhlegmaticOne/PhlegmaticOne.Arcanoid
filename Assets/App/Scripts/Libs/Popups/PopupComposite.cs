using System.Collections.Generic;
using System.Linq;
using Libs.Pooling;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Popups.Factory;
using UnityEngine;

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
            poolBuilder.AddAbstractPool(new UnityAbstractObjectPool<Popup>(CreatePopupPrefabInfos(), parent));
        }

        public IPopupManager CreatePopupManager(IPoolProvider poolProvider)
        {
            var popupFactory = new PopupFactory(poolProvider,
                _parent,
                _popupSystemConfiguration); 
            
            return new PopupManager(popupFactory, poolProvider, _popupSystemConfiguration.StartFromSortingOrder);
        }

        private IEnumerable<PrefabInfo<Popup>> CreatePopupPrefabInfos() =>
            _popupSystemConfiguration.Popups.Select(x => new PrefabInfo<Popup>(x.Popup, _parent));
    }
}