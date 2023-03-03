using Libs.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class WinPopup : Popup
    {
        [SerializeField] private Button _nextLevelButton;
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