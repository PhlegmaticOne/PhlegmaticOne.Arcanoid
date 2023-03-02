using Abstracts.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
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