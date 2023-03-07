using Libs.Popups;
using Libs.Services;
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
        
        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            ConfigureRestartButton();
            ConfigureContinueButton();
            ConfigureBackButton();
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

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                PopupManager.CloseAllPopupsInstant();
                PopupManager.SpawnPopup<PackChoosePopup>();
            });
        }
        
        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
        
        private void ConfigureContinueButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
    }
}