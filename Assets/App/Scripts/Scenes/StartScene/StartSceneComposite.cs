using System.Collections;
using Abstracts.Pooling.Base;
using Abstracts.Pooling.Implementation;
using Abstracts.Popups;
using Abstracts.Popups.Base;
using App.Scripts.Common.Localization;
using App.Scripts.Common.Localization.Base;
using Scenes.Popups;
using UnityEngine;

namespace Scenes.StartScene
{
    public class StartSceneComposite : MonoBehaviour
    {
        [SerializeField] private PopupComposite _popupComposite;

        private IPoolProvider _poolProvider;
        private IPopupManager _popupManager;
        private ILocalizationManager _localizationManager;

        private IEnumerator Start()
        {
            var poolBuilder = PoolBuilder.Create();
            _popupComposite.AddPopupsToPool(poolBuilder);
            _poolProvider = poolBuilder.BuildProvider();
            _popupManager = _popupComposite.CreatePopupManager(_poolProvider);
            
            var localizationManager = new LocalizationManager(new[] { "UI" });
            yield return localizationManager.Initialize();
            _localizationManager = localizationManager;
            
            SpawnStartPopup();
        }

        private void SpawnStartPopup()
        {
            _popupManager.SpawnPopup<StartPopup>(popup =>
            {
                popup.Initialize(_popupManager, _localizationManager);
            });   
        }
    }
}