using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation;
using Game;
using Game.Base;
using Game.Composite;
using Libs.Localization.Installers;
using Libs.Pooling.Implementation;
using Libs.Popups;
using Libs.Popups.Base;
using Libs.Services;
using Popups.Start;
using UnityEngine;

namespace App.Scripts
{
    public class GameComposite : MonoBehaviour
    {
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private MainGameInstaller _mainGameInstaller;
        [SerializeField] private LocalizationManagerInstaller _localizationManagerInstaller;
        [SerializeField] private PackCollectionConfiguration _packCollectionConfiguration;
        [SerializeField] private DefaultPackConfiguration _defaultPackConfiguration;
        
        private IServiceProvider _serviceProvider;
        
        private void Awake()
        {
             _serviceProvider = BuildServices();
             TryInitializePackConfigurations();
             SpawnStartPopup();
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

        private void SpawnStartPopup()
        {
            var popupManager = _serviceProvider.GetRequiredService<IPopupManager>();
            var configuration = _popupComposite.PopupSystemConfiguration;

            if (configuration.SpawnStartPopup)
            {
                popupManager.SpawnPopup(configuration.StartPopup.Popup);
            }
            else
            {
                popupManager.SpawnPopup<StartPopup>();
            }
        }

        private IServiceProvider BuildServices()
        {
            var poolBuilder = PoolBuilder.Create();
            var serviceCollection = new ServiceCollection();
            
            _mainGameInstaller.AddPools(poolBuilder);
            _popupComposite.AddPopupsToPool(poolBuilder);
            
            var poolProvider = poolBuilder.BuildProvider();
            
            var popupManager = _popupComposite.CreatePopupManager(poolProvider, () => ServiceProviderAccessor.ServiceProvider);
            var game = _mainGameInstaller.CreateGame(serviceCollection, poolProvider);
            var localizationManager = _localizationManagerInstaller.CreateLocalizationManagerManager();
            
            var serviceProvider = serviceCollection
                .AddSingleton(poolProvider)
                .AddSingleton(popupManager)
                .AddSingleton(localizationManager)
                .AddSingleton<IPackRepository>(new ResourcesPackRepository(_packCollectionConfiguration, _defaultPackConfiguration))
                .AddSingleton<ILevelRepository>(new ResourcesLevelRepository(_packCollectionConfiguration))
                .AddSingleton<IGame<MainGameData, MainGameEvents>>(game)
                .BuildServiceProvider();
             
            ServiceProviderAccessor.Initialize(serviceProvider);
            return serviceProvider;
        }
    }
}