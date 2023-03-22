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
            _mainGame.Events.HealthAdded += EventsOnHealthAdded;
            _mainGame.Events.HealthLost += EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
        }

        private void MainGameOnStarted()
        {
            var gameData = _objectBag.Get<GameData>();
            var levelData = _objectBag.Get<LevelData>();
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

        private void MainGameOnLost() => _popupManager.SpawnPopup<LosePopup>();
        private void MainGameOnWon() => _popupManager.SpawnPopup<WinPopup>();

        private void OnDisable()
        {
            _mainGame.Started -= MainGameOnStarted;
            _mainGame.Won -= MainGameOnWon;
            _mainGame.Lost -= MainGameOnLost;
            _mainGame.Events.HealthAdded -= EventsOnHealthAdded;
            _mainGame.Events.HealthLost -= EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed -= EventsOnBlockDestroyed;
        }
    }
}