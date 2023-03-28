using Libs.Popups.Controls;

namespace Popups.MainGame.Controls
{
    public class WinControl : ButtonControl
    {
        protected override void OnInteractableSet(bool isInteractable)
        {
            gameObject.SetActive(isInteractable);
        }
    }
}