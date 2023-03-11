using Game.Base;
using Game.ViewModels;
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
        private MainGamePopup _mainGamePopup;

        private WinMenuViewModel _winMenuViewModel;
        
        public void Initialize(IPopupManager popupManager,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _mainGame = mainGame;
            _popupManager = popupManager;
            
            _popupManager.PopupShowed += PopupManagerOnPopupShowed;
            _mainGame.Won += MainGameOnWon;
            _mainGame.Lost += MainGameOnLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
        }

        public void SetupWinViewModel(WinMenuViewModel winMenuViewModel)
        {
            _winMenuViewModel = winMenuViewModel;
        }

        private void PopupManagerOnPopupShowed(Popup popup)
        {
            if (popup is MainGamePopup mainGamePopup)
            {
                _mainGamePopup = mainGamePopup;
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
            var popup = _popupManager.SpawnPopup<WinPopup>();
            popup.SetupViewModel(_winMenuViewModel);
            popup.OnShowing();
            popup.OnClose(() => _mainGamePopup.UpdateHeader());
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