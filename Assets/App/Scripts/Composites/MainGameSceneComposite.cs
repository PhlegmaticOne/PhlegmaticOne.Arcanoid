using System.Collections.Generic;
using Common.Bag;
using Common.Packs.Data.Repositories.Base;
using Common.Scenes;
using Composites.Helpers;
using Composites.Seeding;
using Game;
using Game.Base;
using Game.GameEntities.Controllers;
using Game.PopupRequires.Commands;
using Game.PopupRequires.Commands.Base;
using Game.PopupRequires.ViewModels;
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

        private void OnDestroy()
        {
            ServiceProviderAccessor.Instance.RemoveSceneServiceProvider(SceneIndexes.GameScene);
        }

        private void SetupGame(IServiceProvider global)
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var popupManager = global.GetRequiredService<IPopupManager>();
            var levelRepository = global.GetRequiredService<ILevelRepository>();
            var packRepository = global.GetRequiredService<IPackRepository>();
            var objectBag = global.GetRequiredService<IObjectBag>();
            var factory = gameServices.GetRequiredService<IGameFactory<MainGame>>();
            
            var game = factory.CreateGame();
            var mainPopup = popupManager.SpawnPopup<MainGamePopup>();
            
            mainPopup.SetupViewModel(new MainGameViewModel
            {
                PauseCommand = new PauseGameCommand(game),
                StartCommand = new StartGameCommand(objectBag, levelRepository, game),
                MainMenuViewModel = new MainMenuViewModel
                {
                    ContinueCommand = new ContinueGameCommand(game),
                    RestartCommand = new RestartMainGameCommand(game, objectBag),
                    BackToPackMenuCommand = new CompositeCommand(new ICommand[]
                    {
                        new StopGameCommand(game),
                        new BackToPacksMenuCommand(popupManager)
                    })
                }
            });
            
            _gameController.Initialize(popupManager, game);
            _gameController.SetupWinViewModel(new WinMenuViewModel
            {
                OnShowingCommand = new PauseGameCommand(game),
                OnClosedCommand = new StartGameCommand(objectBag, levelRepository, game),
                OnLastClosedCommand = new CompositeCommand(new ICommand[]
                    {
                        new CloseAllPopupsCommand(popupManager),
                        new BackToPacksMenuCommand(popupManager),
                    }),
                OnNextButtonClickCommand = new CompositeCommand(new ICommand[]
                {
                    new StopGameCommand(game),
                    new SetNextLevelDataCommand(objectBag, packRepository)
                })
            });
            
            _gameController.SetupLoseViewModel(new LosePopupViewModel
            {
                RestartButtonCommand = new RestartMainGameCommand(game, objectBag),
                OnShowingCommand = new PauseGameCommand(game)
            });
        }

        private void MarkNotToSpawnStartPopup()
        {
            _popupSystemConfiguration.DisableStartPopupSpawn();
        }
    }
}

