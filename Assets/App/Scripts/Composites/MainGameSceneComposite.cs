using System.Collections.Generic;
using Common.Bag;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
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
        private ISceneChanger _sceneChanger;
        
        private void Awake()
        {
            ServiceProviderAccessor.SetPrefabPath(ServiceProviderPrefabPath.Instance);
            var serviceProvider = ServiceProviderAccessor.Global;
            ServiceProviderAccessor.Instance.AddSceneServiceProvider(SceneIndexes.GameScene, _gameServices);
            
            _sceneChanger = serviceProvider.GetRequiredService<ISceneChanger>();
            _sceneChanger.SceneChanged += StartGame;
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
            var levelRepository = global.GetRequiredService<ILevelRepository>();
            var levelData = levelRepository.GetLevelData(objectBag.Get<GameData>().PackGameData.PackPersistentData);
            
            objectBag.Set(levelData);
            mainPopup.DisableInput();
            _gameController.Initialize(mainPopup, objectBag, popupManager, game);
        }

        private void StartGame()
        {
            _sceneChanger.SceneChanged -= StartGame;
            var serviceProvider = ServiceProviderAccessor.Global;
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var game = gameServices.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
            var objectBag = serviceProvider.GetRequiredService<IObjectBag>();
            game.StartGame(new MainGameData(objectBag.Get<LevelData>()));
        }

        private void MarkNotToSpawnStartPopup() => 
            _popupSystemConfiguration.DisableStartPopupSpawn();
        
        private void OnDestroy() => 
            ServiceProviderAccessor.Instance.RemoveSceneServiceProvider(SceneIndexes.GameScene);
    }
}

