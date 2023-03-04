using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation;
using Game;
using Game.Base;
using Game.Blocks;
using Game.Blocks.Spawners;
using Game.Field.Builder;
using Libs.Localization;
using Libs.Localization.Base;
using Libs.Pooling;
using Libs.Pooling.Implementation;
using Libs.Popups;
using Libs.Popups.Base;
using Libs.Popups.Initialization;
using Libs.Services;
using Popups.LevelChoose;
using Popups.MainGame;
using Popups.PackChoose;
using Popups.Settings;
using Popups.Start;
using UnityEngine;

namespace App.Scripts
{
    public class GameComposite : MonoBehaviour
    {
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private PackCollectionConfiguration _packCollectionConfiguration;
        [SerializeField] private BlockSpawnerInitializer _blockSpawnerInitializer;
        [SerializeField] private FieldBuilder _fieldBuilder;
        [SerializeField] private Block _block;
        [SerializeField] private Transform _transform;

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
            _serviceProvider.GetRequiredService<IPopupManager>().SpawnPopup<StartPopup>();
        }

        private IServiceProvider BuildServices()
        {
            var poolBuilder = PoolBuilder.Create();
            poolBuilder.AddPool(new UnityObjectPool<Block>(new PrefabInfo<Block>(_block, _transform), 10, 100));
            _popupComposite.AddPopupsToPool(poolBuilder);
            var poolProvider = poolBuilder.BuildProvider();
            var popupManager = _popupComposite.CreatePopupManager(poolProvider, ConfigurePopupInitializers());
            var blockSpawner = _blockSpawnerInitializer.CreateBlockSpawner(poolProvider);
            _fieldBuilder.Initialize(blockSpawner);
            var mainGame = new MainGame(_fieldBuilder);
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton(poolProvider)
                .AddSingleton(popupManager)
                .AddSingleton<ILocalizationManager>(new LocalizationManager(new[] { "UI" }))
                .AddSingleton<IPackRepository>(new ResourcesPackRepository(_packCollectionConfiguration))
                .AddSingleton<ILevelRepository>(new ResourcesLevelRepository(_packCollectionConfiguration))
                .AddSingleton<IGame<MainGameData, MainGameEvents>>(mainGame)
                .BuildServiceProvider();
             
            ServiceProviderAccessor.Initialize(serviceProvider);
            return serviceProvider;
        }

        private IPopupInitializersProvider ConfigurePopupInitializers()
        {
            var builder = _popupComposite.GetPopupInitializersBuilder();
            
            builder.SetInitializerFor<StartPopup>(popup =>
            {
                popup.Initialize(_serviceProvider.GetRequiredService<IPopupManager>());
            });
            
            builder.SetInitializerFor<SettingsPopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<ILocalizationManager>());
            });
            
            builder.SetInitializerFor<PackChoosePopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<IPackRepository>());
            });
            
            builder.SetInitializerFor<LevelChoosePopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<IPackRepository>());
            });
            
            builder.SetInitializerFor<MainGamePopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<ILevelRepository>(),
                    _serviceProvider.GetRequiredService<IGame<MainGameData, MainGameEvents>>());
            });
            
            builder.SetInitializerFor<MainGameMenuPopup>(popup =>
            {
                popup.Initialize(_serviceProvider.GetRequiredService<IPopupManager>());
            });
            
            return builder.BuildPopupInitializersProvider();
        }
    }
}