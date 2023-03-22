using System.Collections.Generic;
using Common.Bag;
using Common.Scenes;
using Composites.Helpers;
using Composites.Seeding;
using Game;
using Game.Base;
using Game.GameEntities.Controllers;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using Popups.MainGame;
using UnityEngine;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Composites
{
    public class MainGameSceneComposite : MonoBehaviour
    {
        [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;
        [SerializeField] private GameController _gameController;
        [SerializeField] private List<ServiceInstaller> _gameServices;
        
        private void Awake()
        {
            ServiceProviderAccessor.SetPrefabPath(ServiceProviderPrefabPath.Instance);
            var serviceProvider = ServiceProviderAccessor.Global;
            ServiceProviderAccessor.Instance.AddSceneServiceProvider(SceneIndexes.GameScene, _gameServices);
            
            GameDataSeed.TrySeedGameData();
            MarkNotToSpawnStartPopup();
            SetupGame(serviceProvider);
        }
        
        private void SetupGame(IServiceProvider global)
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var popupManager = global.GetRequiredService<IPopupManager>();
            var objectBag = global.GetRequiredService<IObjectBag>();
            var game = gameServices.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
            var mainPopup = popupManager.SpawnPopup<MainGamePopup>();
            _gameController.Initialize(mainPopup, objectBag, popupManager, game);
        }

        private void MarkNotToSpawnStartPopup() => 
            _popupSystemConfiguration.DisableStartPopupSpawn();
        
        private void OnDestroy() => 
            ServiceProviderAccessor.Instance.RemoveSceneServiceProvider(SceneIndexes.GameScene);
    }
}

