using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Providers;
using Common.Data.Repositories.Base;
using Game;
using Game.Base;
using Game.Field.Helpers;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Libs.Popups.Base;
using Libs.Services;
using Popups.MainGame;
using UnityEngine;

public class MainGameSceneComposite : MonoBehaviour
{
    [SerializeField] private DefaultPackConfiguration _defaultPackConfiguration;
    
    [SerializeField] private ControlSystem _controlSystem;
    [SerializeField] private Ship _ship;
    [SerializeField] private InteractableZoneSetter _interactableZoneSetter;
    [SerializeField] private Camera _camera;
    
    private void Awake()
    {
        var serviceProvider = ServiceProviderAccessor.ServiceProvider;
        TrySetGameData(serviceProvider);
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
        var gameDataProvider = serviceProvider.GetRequiredService<GameDataProvider>();

        var gameData = gameDataProvider.Get();
        if (gameData != null)
        {
            return;
        }

        var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
        var packConfiguration = _defaultPackConfiguration.DefaultPack;
        var levelCollection = packRepository.GetLevels(packConfiguration.Name);
        var levelId = _defaultPackConfiguration.DefaultLevelIndex;
        gameData = new GameData(packConfiguration, levelCollection, new LevelPreviewData(levelId, false));
        gameDataProvider.Set(gameData);
    }
}
