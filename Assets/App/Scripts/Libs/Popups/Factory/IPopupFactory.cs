namespace Libs.Popups.Factory
{
    public interface IPopupFactory
    {
        T CreatePopup<T>() where T : Popup;
        Popup CreatePopup(Popup prefab);
    }
}