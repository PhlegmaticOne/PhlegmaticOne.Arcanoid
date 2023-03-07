using Libs.Popups;
using Libs.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class WinPopup : Popup
    {
        [SerializeField] private Button _nextLevelButton;
        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            
        }

        public override void EnableInput()
        {
            EnableBehaviour(_nextLevelButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_nextLevelButton);
        }
    }
}