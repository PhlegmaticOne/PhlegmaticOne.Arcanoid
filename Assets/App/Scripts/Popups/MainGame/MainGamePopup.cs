using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game;
using Game.Base;
using Libs.Popups;
using Libs.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGamePopup : Popup
    {
        [SerializeField] private Button _menuButton;

        private IPopupManager _popupManager;
        private IPackRepository _packRepository;
        private ILevelRepository _levelRepository;
        private IGame<MainGameData, MainGameEvents> _mainGame;

        private GameData _gameData;
        private DefaultPackConfiguration _defaultPackConfiguration;

        public void Initialize(IPopupManager popupManager,
            ILevelRepository levelRepository,
            IPackRepository packRepository,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _popupManager = popupManager;
            _packRepository = packRepository;
            _levelRepository = levelRepository;
            _mainGame = mainGame;
            ConfigureMenuButton();
        }

        public void SetDefaultPackConfiguration(DefaultPackConfiguration defaultPackConfiguration) => 
            _defaultPackConfiguration = defaultPackConfiguration;

        public void SetGameData(GameData gameData) => _gameData = gameData;
        
        protected override void OnShow()
        {
            TrySetGameData();
            var levelData = _levelRepository.GetLevelData(_gameData.PackLevelCollection, _gameData.LevelPreviewData); 
            _mainGame.StartGame(new MainGameData(levelData));
        }

        public override void EnableInput() => EnableBehaviour(_menuButton);

        public override void DisableInput() => DisableBehaviour(_menuButton);

        private void ConfigureMenuButton() => _menuButton.onClick.AddListener(() => _popupManager.SpawnPopup<MainGameMenuPopup>());

        public override void Reset() => RemoveAllListeners(_menuButton);

        private void TrySetGameData()
        {
            if (_gameData != null)
            {
                return;
            }

            var packConfiguration = _defaultPackConfiguration.DefaultPack;
            var levels = _packRepository.GetLevels(packConfiguration.Name);
            var level = levels.LevelPreviews[_defaultPackConfiguration.DefaultLevelIndex];
            SetGameData(new GameData(packConfiguration, levels, level));
        }
    }
}