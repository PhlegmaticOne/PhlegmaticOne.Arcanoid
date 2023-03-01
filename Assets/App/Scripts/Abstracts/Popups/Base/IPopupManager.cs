using System;
using UnityEngine.Events;

namespace Abstracts.Popups.Base
{
    public interface IPopupManager
    {
        event UnityAction<Popup> PopupShowed;
        event UnityAction<Popup> PopupHid;
        event UnityAction AllPopupsHid;
        T SpawnPopup<T>() where T : Popup;
        void HidePopup();
        void HideAllPermanent();
    }
}