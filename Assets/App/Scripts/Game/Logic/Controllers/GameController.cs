using Common.Bag;
using Common.Packs.Data.Models;
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
        private IObjectBag _objectBag;
        private IPopupManager _popupManager;
        private MainGamePopup _mainGamePopup;

        private bool _isUpdateForCurrentLevel;

        public void Initialize(MainGamePopup mainGamePopup,
            IObjectBag objectBag,
            IPopupManager popupManager,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _objectBag = objectBag;
            _mainGame = mainGame;
            _mainGamePopup = mainGamePopup;
            _popupManager = popupManager;
            
            _mainGame.Won += MainGameOnWon;
            _mainGame.Lost += MainGameOnLost;
            _mainGame.Started += MainGameOnStarted;
            _mainGame.Initialized += MainGameOnInitialized;
            _mainGame.Events.HealthAdded += EventsOnHealthAdded;
            _mainGame.Events.HealthLost += EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
            
            UpdateInfo();
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
            if (_isUpdateForCurrentLevel)
            {
                return;
            }
            
            var gameData = _objectBag.Get<GameData>();
            var levelData = _objectBag.Get<LevelData>();
            _mainGamePopup.UpdateHeader(gameData);
            _mainGamePopup.InitializeHealthBar(levelData.LifesCount);
            _isUpdateForCurrentLevel = true;
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
            _isUpdateForCurrentLevel = false;
            var losePopup = _popupManager.SpawnPopup<LosePopup>();
        }
        
        private void MainGameOnWon()
        {
            _isUpdateForCurrentLevel = false;
            var winPopup = _popupManager.SpawnPopup<WinPopup>();
        }

        private void OnDisable()
        {
            _mainGame.Won -= MainGameOnWon;
            _mainGame.Lost -= MainGameOnLost;
            _mainGame.Started -= MainGameOnStarted;
            _mainGame.Initialized -= MainGameOnInitialized;
            _mainGame.Events.HealthAdded -= EventsOnHealthAdded;
            _mainGame.Events.HealthLost -= EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed -= EventsOnBlockDestroyed;
        }
    }
}