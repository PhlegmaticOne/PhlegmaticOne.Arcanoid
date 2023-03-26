using System.Collections.Generic;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
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
            ServiceProviderAccessor.Instance.AddSceneServiceProvider(SceneNames.Game, _gameServices);
            
            GameDataSeed.TrySeedGameData();
            MarkNotToSpawnStartPopup();
            SetupGame(serviceProvider);
            
            _sceneChanger = serviceProvider.GetRequiredService<ISceneChanger>();
            if (_sceneChanger.CurrentScene != null)
            {
                _sceneChanger.SceneChanged += StartGame;
            }
            else
            {
                StartGame();
            }
        }

        private void SetupGame(IServiceProvider global)
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var popupManager = global.GetRequiredService<IPopupManager>();
            var objectBag = global.GetRequiredService<IGameDataProvider>();
            var game = gameServices.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
            var mainPopup = popupManager.SpawnPopup<MainGamePopup>();
            var levelRepository = global.GetRequiredService<ILevelRepository>();
            var levelData = levelRepository.GetLevelData(objectBag.GetGameData().PackGameData.PackPersistentData);
            
            objectBag.SetNewLevel(levelData);
            mainPopup.DisableInput();
            _gameController.Initialize(mainPopup, objectBag, popupManager, game);
        }

        private void StartGame()
        {
            _sceneChanger.SceneChanged -= StartGame;
            var serviceProvider = ServiceProviderAccessor.Global;
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            
            var game = gameServices.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
            var objectBag = serviceProvider.GetRequiredService<IGameDataProvider>();
            game.StartGame(new MainGameData(objectBag.GetGameData().CurrentLevel));
        }

        private void MarkNotToSpawnStartPopup() => _popupSystemConfiguration.DisableStartPopupSpawn();
        
        private void OnDestroy() => 
            ServiceProviderAccessor.Instance.RemoveSceneServiceProvider(SceneNames.Game);
    }
}

