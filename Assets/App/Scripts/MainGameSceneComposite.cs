using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game;
using Game.Accessors;
using Game.Base;
using Game.Field.Helpers;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Services;
using Popups.MainGame;
using UnityEngine;

public class MainGameSceneComposite : MonoBehaviour
{
    [SerializeField] private PopupSystemConfiguration _popupSystemConfiguration;
    
    [SerializeField] private ControlSystem _controlSystem;
    [SerializeField] private Ship _ship;
    [SerializeField] private InteractableZoneSetter _interactableZoneSetter;
    [SerializeField] private Camera _camera;
    
    private void Awake()
    {
        ServiceProviderAccessor.SetPrefabPath("App/ServiceProvider/ServiceProviderAccessor");
        var serviceProvider = ServiceProviderAccessor.ServiceProvider;
        
        TrySetGameData(serviceProvider);
        MarkNotToSpawnStartPopup();
        StartGame(serviceProvider);
    }

    private void StartGame(IServiceProvider serviceProvider)
    {
        var popupManager = serviceProvider.GetRequiredService<IPopupManager>();
        var factory = serviceProvider.GetRequiredService<IGameFactory<MainGameRequires, MainGame>>();
        
        factory.SetupGameRequires(new MainGameRequires
        {
            ControlSystem = _controlSystem,
            Ship = _ship,
            InteractableZoneSetter = _interactableZoneSetter,
            Camera = _camera
        });
        var game = factory.CreateGame();
        
        var mainGamePopup = popupManager.SpawnPopup<MainGamePopup>();
        mainGamePopup.SetupGame(game);
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
