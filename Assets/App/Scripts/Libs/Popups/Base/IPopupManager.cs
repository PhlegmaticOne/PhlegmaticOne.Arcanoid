using UnityEngine.Events;

namespace Libs.Popups.Base
{
    public interface IPopupManager
    {
        Popup CurrentPopup { get; }
        T SpawnPopup<T>() where T : Popup;
        Popup SpawnPopup(Popup prefab);
        void CloseLastPopup();
        void ClosePopup(Popup popup);
        void CloseLastPopupInstant();
        void ClosePopupInstant(Popup popup);
        void CloseAllPopupsInstant();
    }
}