using System.Linq;
using Common.Data.Repositories.Base;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using UnityEngine;

public class StartSceneComposite : MonoBehaviour
{
    [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;
    
    private void Awake()
    {
        var serviceProvider = ServiceProviderAccessor.ServiceProvider;
        TryInitializePackConfigurations(serviceProvider);
        TrySpawnStartPopup(serviceProvider);
    }
    
    private void TryInitializePackConfigurations(IServiceProvider serviceProvider)
    {
        var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
        
        if (packRepository.PacksInitialized)
        {
            return;
        }
        
        var packConfigurations = packRepository.GetAll().ToList();
        
        foreach (var packConfiguration in packConfigurations)
        {
            var levelsCount = packRepository.GetLevelsCount(packConfiguration.Name);
            packConfiguration.SetLevelsCount(levelsCount);
            packRepository.Save(packConfiguration);
        }
        
        packRepository.MarkAsInitialized();
        packRepository.Save();
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
