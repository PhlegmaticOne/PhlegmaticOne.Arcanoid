using System.Collections.Generic;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Common.Scenes;
using Game;
using Game.Accessors;
using Game.Base;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.PackChoose;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGamePopup : Popup, ILocalizable
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private LocalizationContext _localizationContext;
        private IPackRepository _packRepository;
        private ILevelRepository _levelRepository;
        private IObjectAccessor<GameData> _gameDataProvider;
        private ILocalizationManager _localizationManager;
        private IGame<MainGameData, MainGameEvents> _mainGame;

        private MainGameData _currentGameData;
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _levelRepository = serviceProvider.GetRequiredService<ILevelRepository>();
            _gameDataProvider = serviceProvider.GetRequiredService<IObjectAccessor<GameData>>();
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            ConfigureMenuButton();
        }

        public override void EnableInput() => EnableBehaviour(_menuButton);

        protected override void OnShowed()
        {
            var gameData = _gameDataProvider.Get();
            var levelData = _levelRepository.GetLevelData(gameData.PackLevelCollection, gameData.LevelPreviewData);
            _currentGameData = new MainGameData(levelData);
            _mainGame.StartGame(_currentGameData);
        }

        public override void DisableInput() => DisableBehaviour(_menuButton);

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_menuButton);
        }

        public void SetupGame(IGame<MainGameData, MainGameEvents> mainGame) => _mainGame = mainGame;

        private void ConfigureMenuButton()
        {
            _menuButton.onClick.AddListener(() =>
            {
                _mainGame.Pause();
                var menuPopup = PopupManager.SpawnPopup<MainGameMenuPopup>();
                menuPopup.OnBack(() =>
                {
                    _mainGame.Stop();
                    var sceneChanger = new SceneChanger<PackChoosePopup>(PopupManager);
                    sceneChanger.ChangeScene(0);
                });
                menuPopup.OnRestart(() =>
                {
                    _mainGame.Stop();
                    _mainGame.StartGame(_currentGameData);
                    _mainGame.Unpause();
                });
                menuPopup.OnContinue(() => _mainGame.Unpause());
            });
        }
    }
}