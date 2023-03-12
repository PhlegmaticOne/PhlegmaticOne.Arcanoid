using Game.Base;
using Game.PopupRequires.ViewModels;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.MainGame;
using UnityEngine;

namespace Game.GameEntities.Controllers
{
    public class GameController : MonoBehaviour
    {
        private IGame<MainGameData, MainGameEvents> _mainGame;
        private IPopupManager _popupManager;
        private MainGamePopup _mainGamePopup;

        private WinMenuViewModel _winMenuViewModel;
        private LosePopupViewModel _losePopupViewModel;
        
        public void Initialize(IPopupManager popupManager, IGame<MainGameData, MainGameEvents> mainGame)
        {
            _mainGame = mainGame;
            _popupManager = popupManager;
            
            _popupManager.PopupShowed += PopupManagerOnPopupShowed;
            _mainGame.Won += MainGameOnWon;
            _mainGame.Lost += MainGameOnLost;
            _mainGame.Events.HealthAdded += EventsOnHealthAdded;
            _mainGame.Events.HealthLost += EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed += EventsOnBlockDestroyed;
        }

        private void EventsOnHealthLost()
        {
            _mainGamePopup.HealthBarView.LoseHealth();
        }

        private void EventsOnHealthAdded()
        {
            _mainGamePopup.HealthBarView.AddHealth();
        }

        public void SetupWinViewModel(WinMenuViewModel winMenuViewModel)
        {
            _winMenuViewModel = winMenuViewModel;
        }
        
        public void SetupLoseViewModel(LosePopupViewModel losePopupViewModel)
        {
            _losePopupViewModel = losePopupViewModel;
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
            var popup = _popupManager.SpawnPopup<LosePopup>();
            popup.SetupViewModel(_losePopupViewModel);
            popup.OnShowing();
            popup.OnClose(() =>
            {
                _mainGamePopup.UpdateHeader();
                _mainGamePopup.InitializeHealthBar();
            });
        }

        private void MainGameOnWon()
        {
            var popup = _popupManager.SpawnPopup<WinPopup>();
            popup.SetupViewModel(_winMenuViewModel);
            popup.OnShowing();
            popup.OnClose(() =>
            {
                _mainGamePopup.UpdateHeader();
                _mainGamePopup.InitializeHealthBar();
            });
        }

        private void OnDisable()
        {
            _popupManager.PopupShowed -= PopupManagerOnPopupShowed;
            _mainGame.Won -= MainGameOnWon;
            _mainGame.Lost -= MainGameOnLost;
            _mainGame.Events.HealthAdded -= EventsOnHealthAdded;
            _mainGame.Events.HealthLost -= EventsOnHealthLost;
            _mainGame.Events.BlockDestroyed -= EventsOnBlockDestroyed;
        }
    }
}