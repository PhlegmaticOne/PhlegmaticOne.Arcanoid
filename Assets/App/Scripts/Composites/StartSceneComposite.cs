using Composites.Helpers;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using UnityEngine;

namespace Composites
{
    public class StartSceneComposite : MonoBehaviour
    {
        [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;

        private void Awake()
        {
            ServiceProviderAccessor.SetPrefabPath(ServiceProviderPrefabPath.Instance);
            var serviceProvider = ServiceProviderAccessor.Global;
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
