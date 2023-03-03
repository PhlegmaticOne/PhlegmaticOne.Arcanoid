using UnityEngine.Events;

namespace Libs.Popups.Initialization
{
    public interface IPopupInitializersBuilder
    {
        void SetInitializerFor<TPopup>(UnityAction<TPopup> initializer) where TPopup : Popup;
        IPopupInitializersProvider BuildPopupInitializersProvider();
    }
}