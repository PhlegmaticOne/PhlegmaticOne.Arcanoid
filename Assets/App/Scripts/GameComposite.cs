using System.Collections;
using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation;
using Libs.Localization;
using Libs.Localization.Base;
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

        private IServiceProvider _serviceProvider;
        
        private IEnumerator Start()
        {
             var poolBuilder = PoolBuilder.Create();
             _popupComposite.AddPopupsToPool(poolBuilder);
             var poolProvider = poolBuilder.BuildProvider();
             var popupManager = _popupComposite.CreatePopupManager(poolProvider, ConfigurePopupInitializers());
            
             var localizationManager = new LocalizationManager(new[] { "UI" });
             yield return localizationManager.Initialize();

             var serviceProvider = new ServiceCollection()
                 .AddSingleton(poolProvider)
                 .AddSingleton(popupManager)
                 .AddSingleton<ILocalizationManager>(localizationManager)
                 .AddSingleton<IPackRepository>(new ResourcesPackRepository(_packCollectionConfiguration))
                 .AddSingleton<ILevelRepository>(new ResourcesLevelRepository(_packCollectionConfiguration))
                 .BuildServiceProvider();
             
             ServiceProviderAccessor.Initialize(serviceProvider);
             _serviceProvider = serviceProvider;

             popupManager.SpawnPopup<StartPopup>();
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
                popup.Initialize(_serviceProvider.GetRequiredService<IPopupManager>());
            });
            
            builder.SetInitializerFor<MainGameMenuPopup>(popup =>
            {
                popup.Initialize(_serviceProvider.GetRequiredService<IPopupManager>());
            });
            
            return builder.BuildPopupInitializersProvider();
        }
    }
}