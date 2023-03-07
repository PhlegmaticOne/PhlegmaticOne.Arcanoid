using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game;
using Game.Base;
using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGamePopup : Popup
    {
        [SerializeField] private Button _menuButton;

        private IPackRepository _packRepository;
        private ILevelRepository _levelRepository;
        private IGame<MainGameData, MainGameEvents> _mainGame;

        private GameData _gameData;
        private DefaultPackConfiguration _defaultPackConfiguration;
        
        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _levelRepository = serviceProvider.GetRequiredService<ILevelRepository>();
            _mainGame = serviceProvider.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
            ConfigureMenuButton();
        }
        
        protected override void OnShowed()
        {
            TrySetGameData();
            var levelData = _levelRepository.GetLevelData(_gameData.PackLevelCollection, _gameData.LevelPreviewData); 
            _mainGame.StartGame(new MainGameData(levelData));
        }
        
        public override void EnableInput() => EnableBehaviour(_menuButton);
        
        public override void DisableInput() => DisableBehaviour(_menuButton);

        public override void Reset() => RemoveAllListeners(_menuButton);
        
        
        public void SetGameData(GameData gameData) => _gameData = gameData;

        private void TrySetGameData()
        {
            if (_gameData != null)
            {
                return;
            }

            var packConfiguration = _packRepository.DefaultPackConfiguration.DefaultPack;
            var levels = _packRepository.GetLevels(packConfiguration.Name);
            var level = levels.LevelPreviews[_defaultPackConfiguration.DefaultLevelIndex];
            SetGameData(new GameData(packConfiguration, levels, level));
        }
        
        private void ConfigureMenuButton()
        {
            _menuButton.onClick.AddListener(() => PopupManager.SpawnPopup<MainGameMenuPopup>());
        }
    }
}