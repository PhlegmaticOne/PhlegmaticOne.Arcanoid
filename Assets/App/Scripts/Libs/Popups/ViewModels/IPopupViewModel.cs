using Libs.Popups.ViewModels.Actions;

namespace Libs.Popups.ViewModels
{
    public interface IPopupViewModel
    {
        IPopupAction ShowAction { get; set; }
        IPopupAction CloseAction { get; set; }
    }
}