using UnityEngine.Events;

namespace Abstracts.Popups.Initialization
{
    public interface IPopupInitializersBuilder
    {
        void SetInitializerFor<TPopup>(UnityAction<TPopup> initializer) where TPopup : Popup;
        IPopupInitializersProvider BuildPopupInitializersProvider();
    }
}