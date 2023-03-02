using Abstracts.Popups;
using Abstracts.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
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
                _popupManager.HideAllPermanent();
                _popupManager.SpawnPopup<ChoosePackPopup>();
            });
            _restartButton.onClick.AddListener(() => _popupManager.HidePopup());
            _continueButton.onClick.AddListener(() => _popupManager.HidePopup());
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