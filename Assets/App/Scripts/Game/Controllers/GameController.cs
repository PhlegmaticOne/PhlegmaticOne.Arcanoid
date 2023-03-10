using Common.Data.Models;
using Game.Accessors;
using Game.Base;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.MainGame;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        private IGame<MainGameData, MainGameEvents> _mainGame;
        private IPopupManager _popupManager;
        private IObjectAccessor<GameData> _gameDataAccessor;
        private MainGamePopup _mainGamePopup;
        
        public void Initialize(IPopupManager popupManager,
            IGame<MainGameData, MainGameEvents> mainGame,
            IObjectAccessor<GameData> gameDataAccessor)
        {
            _mainGame = mainGame;
            _popupManager = popupManager;
            _gameDataAccessor = gameDataAccessor;
            
            _popupManager.PopupShowed += PopupManagerOnPopupShowed;
            _mainGame.Won += MainGameOnWon;
            _mainGame.Lost += MainGameOnLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
        }

        private void PopupManagerOnPopupShowed(Popup popup)
        {
            if (popup is MainGamePopup mainGamePopup)
            {
                _mainGamePopup = mainGamePopup;
                var gameData = _gameDataAccessor.Get();
                _mainGamePopup.UpdatePackInfoView(gameData.PackConfiguration);
                _mainGamePopup.UpdateLevelPassPercentageView(0);
            }
        }

        private void EventsOnBlockDestroyed(BlockDestroyedEventArgs args)
        {
            var normalizedPercentage = 1 - (float)args.RemainBlocksCount / args.ActiveBlocksCount;
            _mainGamePopup.UpdateLevelPassPercentageView(normalizedPercentage);
        }

        private void MainGameOnLost()
        {
            _mainGame.Pause();
            var popup = _popupManager.SpawnPopup<LosePopup>();
        }

        private void MainGameOnWon()
        {
            _mainGame.Pause();
            var popup = _popupManager.SpawnPopup<WinPopup>();
            popup.UpdatePackInfoView(_gameDataAccessor.Get().PackConfiguration);
        }

        private void OnDisable()
        {
            _popupManager.PopupShowed -= PopupManagerOnPopupShowed;
            _mainGame.Won -= MainGameOnWon;
            _mainGame.Lost -= MainGameOnLost;
            _mainGame.Events.BlockDestroyed -= EventsOnBlockDestroyed;
        }
    }
}