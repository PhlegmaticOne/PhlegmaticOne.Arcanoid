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
        private ILevelRepository _levelRepository;
        private IGame<MainGameData, MainGameEvents> _mainGame;

        private GameData _gameData;

        public void Initialize(IPopupManager popupManager,
            ILevelRepository levelRepository,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _popupManager = popupManager;
            _levelRepository = levelRepository;
            _mainGame = mainGame;
            ConfigureMenuButton();
        }

        public void SetGameData(GameData gameData) => _gameData = gameData;
        
        protected override void OnShow()
        {
            var levelData = _levelRepository.GetLevelData(_gameData.PackLevelCollection, _gameData.LevelPreviewData); 
            _mainGame.StartGame(new MainGameData(levelData));
        }

        public override void EnableInput() => EnableBehaviour(_menuButton);

        public override void DisableInput() => DisableBehaviour(_menuButton);

        private void ConfigureMenuButton() => _menuButton.onClick.AddListener(() => _popupManager.SpawnPopup<MainGameMenuPopup>());

        public override void Reset() => RemoveAllListeners(_menuButton);
    }
}