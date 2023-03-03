using UnityEngine.Events;

namespace Libs.Popups.Base
{
    public interface IPopupManager
    {
        event UnityAction<Popup> PopupShowed;
        event UnityAction<Popup> PopupHid;
        event UnityAction AllPopupsHid;
        T SpawnPopup<T>() where T : Popup;
        void HidePopup();
        void HidePopupPermanent();
        void HideAllPermanent();
    }
}