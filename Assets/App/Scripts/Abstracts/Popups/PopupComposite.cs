using System.Collections.Generic;
using System.Linq;
using Abstracts.Pooling;
using Abstracts.Pooling.Base;
using Abstracts.Pooling.Implementation;
using Abstracts.Popups.Animations;
using Abstracts.Popups.Base;
using Abstracts.Popups.Configurations;
using Abstracts.Popups.Initialization;
using UnityEngine;

namespace Abstracts.Popups
{
    public class PopupComposite : MonoBehaviour
    {
        [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;
        [SerializeField] private RectTransform _mainCanvasTransform;

        public void AddPopupsToPool(PoolBuilder poolBuilder)
        {
            poolBuilder.AddAbstractPool(new UnityAbstractObjectPool<Popup>(CreatePopupPrefabInfos()));
        }

        public IPopupInitializersBuilder GetPopupInitializersBuilder() => new PopupInitializersBuilder();

        public IPopupManager CreatePopupManager(IPoolProvider poolProvider, IPopupInitializersProvider popupInitializersProvider)
        {
            var appearanceAnimationsFactory = new AppearanceAnimationsFactory();
            var disappearanceAnimationsFactory = new DisappearanceAnimationsFactory();
            return new PopupManager(poolProvider, 
                popupInitializersProvider,
                appearanceAnimationsFactory,
                disappearanceAnimationsFactory, 
                _mainCanvasTransform,
                _popupSystemConfiguration.StartFromSortingOrder);
        }

        private IEnumerable<PrefabInfo<Popup>> CreatePopupPrefabInfos() =>
            _popupSystemConfiguration.Popups.Select(x => new PrefabInfo<Popup>(x, _mainCanvasTransform));
    }
}