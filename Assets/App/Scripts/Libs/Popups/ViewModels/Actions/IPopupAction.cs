using Libs.Popups.Animations.Base;
using Libs.Popups.ViewModels.Commands;

namespace Libs.Popups.ViewModels.Actions
{
    public interface IPopupAction : IAction
    {
        IPopupAnimation PopupAnimation { get; }
        ICommand BeforeActionCommand { get; set; }
        ICommand AfterActionCommand { get; set; }
    }
}