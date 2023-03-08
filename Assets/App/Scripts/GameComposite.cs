using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Providers;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation;
using Game.Composite;
using Libs.Localization.Installers;
using Libs.Pooling.Implementation;
using Libs.Popups;
using Libs.Popups.Base;
using Libs.Services;
using UnityEngine;

namespace App.Scripts
{
    public class GameComposite : MonoBehaviour
    {
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private MainGameFactoryInstaller _mainGameInstaller;
        [SerializeField] private LocalizationManagerInstaller _localizationManagerInstaller;
        [SerializeField] private PackCollectionConfiguration _packCollectionConfiguration;
        [SerializeField] private DefaultPackConfiguration _defaultPackConfiguration;
        
        private IServiceProvider _serviceProvider;
        
        private void Awake()
        {
            var serviceProvider = ServiceProviderAccessor.ServiceProvider;
            _serviceProvider = serviceProvider ?? BuildServices();
            TryInitializePackConfigurations();
            TrySpawnStartPopup();
        }
        
        private void TryInitializePackConfigurations()
        {
            var packRepository = _serviceProvider.GetRequiredService<IPackRepository>();
            
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

        private void TrySpawnStartPopup()
        {
            var popupManager = _serviceProvider.GetRequiredService<IPopupManager>();
            var configuration = _popupComposite.PopupSystemConfiguration;

            if (configuration.SpawnStartPopup)
            {
                popupManager.SpawnPopup(configuration.StartPopup.Popup);
                configuration.DisableStartPopupSpawn();
            }
        }

        private IServiceProvider BuildServices()
        {
            var serviceCollection = new ServiceCollection();
            
            var poolBuilder = PoolBuilder.Create();
            _mainGameInstaller.AddPools(poolBuilder);
            _popupComposite.AddPopupsToPool(poolBuilder);
            var poolProvider = poolBuilder.BuildProvider();
            
            var popupManager = _popupComposite.CreatePopupManager(poolProvider, () => ServiceProviderAccessor.ServiceProvider);
            var localizationManager = _localizationManagerInstaller.CreateLocalizationManager();
            var gameFactory = _mainGameInstaller.CreateGame(poolProvider);
            
            var serviceProvider = serviceCollection
                .AddSingleton(new GameDataProvider())
                .AddSingleton(poolProvider)
                .AddSingleton(popupManager)
                .AddSingleton(localizationManager)
                .AddSingleton(gameFactory)
                .AddSingleton<IPackRepository>(new ResourcesPackRepository(_packCollectionConfiguration, _defaultPackConfiguration))
                .AddSingleton<ILevelRepository>(new ResourcesLevelRepository(_packCollectionConfiguration))
                .BuildServiceProvider();
             
            ServiceProviderAccessor.Initialize(serviceProvider);
            return serviceProvider;
        }
    }
}