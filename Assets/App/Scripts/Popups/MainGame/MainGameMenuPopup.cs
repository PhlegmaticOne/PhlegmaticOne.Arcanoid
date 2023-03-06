using Libs.Popups;
using Libs.Popups.Base;
using Popups.PackChoose;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class MainGameMenuPopup : Popup
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        private IPopupManager _popupManager;

        public void Initialize(IPopupManager popupManager)
        {
            _popupManager = popupManager;
            _backButton.onClick.AddListener(() =>
            {
                _popupManager.CloseAllPopupsInstant();
                _popupManager.SpawnPopup<PackChoosePopup>();
            });
            _restartButton.onClick.AddListener(() => _popupManager.CloseLastPopup());
            _continueButton.onClick.AddListener(() => _popupManager.CloseLastPopup());
        }
        

        public override void EnableInput()
        {
            EnableBehaviour(_backButton);
            EnableBehaviour(_restartButton);
            EnableBehaviour(_continueButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_backButton);
            DisableBehaviour(_restartButton);
            DisableBehaviour(_continueButton);
        }

        public override void Reset()
        {
            RemoveAllListeners(_backButton);
            RemoveAllListeners(_restartButton);
            RemoveAllListeners(_continueButton);
        }
    }
}