using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class LosePopup : Popup
    {
        [SerializeField] private Button _restartButton;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            ConfigureRestartButton();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_restartButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_restartButton);
        }

        public override void Reset()
        {
            RemoveAllListeners(_restartButton);
        }

        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() => PopupManager.CloseLastPopup());
        }
    }
}