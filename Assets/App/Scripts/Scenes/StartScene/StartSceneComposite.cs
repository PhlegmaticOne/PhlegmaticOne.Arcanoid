using System.Collections;
using Abstracts.Pooling.Implementation;
using Abstracts.Popups;
using Abstracts.Popups.Base;
using Abstracts.Popups.Initialization;
using Abstracts.Services;
using Common.Localization;
using Common.Localization.Base;
using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data.Repositories.Base;
using Scenes.MainGameScene.Data.Repositories.ResourcesImplementation;
using Scenes.Popups;
using UnityEngine;

namespace Scenes.StartScene
{
    public class StartSceneComposite : MonoBehaviour
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

             //popupManager.SpawnPopup<StartPopup>();
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
            
            builder.SetInitializerFor<Popups.ChoosePackPopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<IPackRepository>());
            });
            
            builder.SetInitializerFor<ChooseLevelPopup>(popup =>
            {
                popup.Initialize(
                    _serviceProvider.GetRequiredService<IPopupManager>(),
                    _serviceProvider.GetRequiredService<IPackRepository>());
            });
            
            return builder.BuildPopupInitializersProvider();
        }
    }
}