using System.Collections.Generic;

namespace Libs.Popups.Base
{
    public interface IPopupManager
    {
        IList<Popup> GetAll();
        Popup CurrentPopup { get; }
        T SpawnPopup<T>() where T : Popup;
        Popup SpawnPopup(Popup prefab);
        void ClosePopup(Popup popup, bool enablePreviousPopupInput = true);
        void CloseLastPopup(bool enablePreviousPopupInput = true);
        void ClosePopupInstant(Popup popup);
        void CloseLastPopupInstant();
        void CloseAllPopupsInstant();
    }
}