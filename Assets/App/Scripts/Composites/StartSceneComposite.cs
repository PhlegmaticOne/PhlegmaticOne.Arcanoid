using System;
using System.Collections;
using Common.Scenes;
using Composites.Helpers;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using UnityEngine;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Composites
{
    public class StartSceneComposite : MonoBehaviour
    {
        [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;

        private void Awake()
        {
            ServiceProviderAccessor.SetPrefabPath(ServiceProviderPrefabPath.Instance);
            var serviceProvider = ServiceProviderAccessor.Global;
            serviceProvider.GetRequiredService<ISceneChanger>();
            TrySpawnStartPopup(serviceProvider);
        }

        private void TrySpawnStartPopup(IServiceProvider serviceProvider)
        {
            var popupManager = serviceProvider.GetRequiredService<IPopupManager>();

            if (_popupSystemConfiguration.SpawnStartPopup)
            {
                popupManager.SpawnPopup(_popupSystemConfiguration.StartPopup.Popup);
                _popupSystemConfiguration.DisableStartPopupSpawn();
            }
        }
    }
}
