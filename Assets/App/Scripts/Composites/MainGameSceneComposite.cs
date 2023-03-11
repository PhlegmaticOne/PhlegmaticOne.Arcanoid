using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game;
using Game.Accessors;
using Game.Base;
using Game.Commands;
using Game.Commands.Base;
using Game.Controllers;
using Game.Field.Helpers;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Game.ViewModels;
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
        
        [SerializeField] private ControlSystem _controlSystem;
        [SerializeField] private Ship _ship;
        [SerializeField] private InteractableZoneSetter _interactableZoneSetter;
        [SerializeField] private Camera _camera;

        [SerializeField] private GameController _gameController;
        
        private void Awake()
        {
            ServiceProviderAccessor.SetPrefabPath("App/ServiceProvider/ServiceProviderAccessor");
            var serviceProvider = ServiceProviderAccessor.ServiceProvider;
            
            TrySetGameData(serviceProvider);
            MarkNotToSpawnStartPopup();
            SetupGame(serviceProvider);
        }

        private void SetupGame(IServiceProvider serviceProvider)
        {
            var popupManager = serviceProvider.GetRequiredService<IPopupManager>();
            var factory = serviceProvider.GetRequiredService<IGameFactory<MainGameRequires, MainGame>>();
            var levelRepository = serviceProvider.GetRequiredService<ILevelRepository>();
            var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            var gameDataAccessor = serviceProvider.GetRequiredService<IObjectAccessor<GameData>>();
            var levelDataAccessor = serviceProvider.GetRequiredService<IObjectAccessor<LevelData>>();
            
            factory.SetupGameRequires(new MainGameRequires
            {
                ControlSystem = _controlSystem,
                Ship = _ship,
                InteractableZoneSetter = _interactableZoneSetter,
                Camera = _camera
            });
            var game = factory.CreateGame();
            var mainPopup = popupManager.SpawnPopup<MainGamePopup>();
            
            mainPopup.SetupViewModel(new MainGameViewModel
            {
                PauseCommand = new PauseGameCommand(game),
                StartCommand = new StartGameCommand(gameDataAccessor, levelRepository, levelDataAccessor, game),
                MainMenuViewModel = new MainMenuViewModel
                {
                    ContinueCommand = new ContinueGameCommand(game),
                    RestartCommand = new RestartMainGameCommand(game, levelDataAccessor),
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
                OnClosedCommand = new StartGameCommand(gameDataAccessor, levelRepository, levelDataAccessor, game),
                OnLastClosedCommand = new CompositeCommand(new ICommand[]
                    {
                        new CloseAllPopupsCommand(popupManager),
                        new BackToPacksMenuCommand(popupManager),
                    }),
                OnNextButtonClickCommand = new CompositeCommand(new ICommand[]
                {
                    new StopGameCommand(game),
                    new SetNextLevelDataCommand(gameDataAccessor, packRepository)
                })
            });
        }
        
        private void TrySetGameData(IServiceProvider serviceProvider)
        {
            var gameDataProvider = serviceProvider.GetRequiredService<IObjectAccessor<GameData>>();
            
            var gameData = gameDataProvider.Get();
            if (gameData != null)
            {
                return;
            }
            
            var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            var defaultPackConfiguration = packRepository.DefaultPackConfiguration;
            var defaultPack = defaultPackConfiguration.DefaultPack;
            
            if (defaultPack.IsPassed)
            {
                defaultPack.ResetPassedLevelsCount();
            }
            
            var levelCollection = packRepository.GetLevels(defaultPackConfiguration.DefaultPack.Name);
            var levelId = defaultPackConfiguration.DefaultLevelIndex;
            gameData = new GameData(defaultPackConfiguration.DefaultPack, levelCollection, new LevelPreviewData(levelId, false));
            gameDataProvider.Set(gameData);
        }

        private void MarkNotToSpawnStartPopup()
        {
            _popupSystemConfiguration.DisableStartPopupSpawn();
        }
    }
}

