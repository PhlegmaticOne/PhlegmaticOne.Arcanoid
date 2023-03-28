using Common.Game.Providers.Providers;
using Game.Base;
using Libs.Popups.Base;
using Popups.Lose;
using Popups.MainGame;
using Popups.Win;
using UnityEngine;

namespace Game.GameEntities.Controllers
{
    public class GameController : MonoBehaviour
    {
        private IGame<MainGameData, MainGameEvents> _mainGame;
        private IGameDataProvider _gameDataProvider;
        private IPopupManager _popupManager;
        private MainGamePopup _mainGamePopup;

        public void Initialize(MainGamePopup mainGamePopup,
            IGameDataProvider gameDataProvider,
            IPopupManager popupManager,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _gameDataProvider = gameDataProvider;
            _mainGame = mainGame;
            _mainGamePopup = mainGamePopup;
            _popupManager = popupManager;
            
            _mainGame.Won += MainGameOnWon;
            _mainGame.PreWon += MainGameOnPreWon;
            _mainGame.Lost += MainGameOnLost;
            _mainGame.Started += MainGameOnStarted;
            _mainGame.Initialized += MainGameOnInitialized;
            _mainGame.Events.HealthAdded += EventsOnHealthAdded;
            _mainGame.Events.HealthLost += EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
            
            UpdateInfo();
        }

        private void MainGameOnPreWon()
        {
            _mainGamePopup.DisableInput();
        }

        private void MainGameOnInitialized()
        {
            UpdateInfo();
        }

        private void MainGameOnStarted()
        {
            _mainGamePopup.EnableInput();
        }

        private void UpdateInfo()
        {
            var gameData = _gameDataProvider.GetGameData();
            var levelData = gameData.CurrentLevel;
            _mainGamePopup.UpdateHeader(gameData);
            _mainGamePopup.InitializeHealthBar(levelData.LifesCount);
        }

        private void EventsOnHealthLost()
        {
            _mainGamePopup.HealthBarView.LoseHealth();
        }

        private void EventsOnHealthAdded()
        {
            _mainGamePopup.HealthBarView.AddHealth();
        }

        private void EventsOnBlockDestroyed(BlockDestroyedEventArgs args)
        {
            var normalizedPercentage = 1 - (float)args.RemainBlocksCount / args.ActiveBlocksCount;
            _mainGamePopup.UpdateLevelPassPercentageView(normalizedPercentage);
        }

        private void MainGameOnLost()
        {
            _popupManager.SpawnPopup<LosePopup>();
        }
        
        private void MainGameOnWon()
        {
            _popupManager.SpawnPopup<WinPopup>();
        }

        private void OnDisable()
        {
            _mainGame.Won -= MainGameOnWon;
            _mainGame.PreWon -= MainGameOnPreWon;
            _mainGame.Lost -= MainGameOnLost;
            _mainGame.Started -= MainGameOnStarted;
            _mainGame.Initialized -= MainGameOnInitialized;
            _mainGame.Events.HealthAdded -= EventsOnHealthAdded;
            _mainGame.Events.HealthLost -= EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed -= EventsOnBlockDestroyed;
        }
    }
}