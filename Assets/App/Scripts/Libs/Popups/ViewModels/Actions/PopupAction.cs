using Libs.Popups.Animations.Base;
using Libs.Popups.ViewModels.Commands;

namespace Libs.Popups.ViewModels.Actions
{
    public class PopupAction : IPopupAction
    {
        public static IPopupAction Empty => new PopupAction(NoneCommand.New, NoneCommand.New);
        public PopupAction(ICommand onBeforeActionCommand, ICommand onAfterActionCommand)
        {
            BeforeActionCommand = onBeforeActionCommand;
            AfterActionCommand = onAfterActionCommand;
        }
        
        public IPopupAnimation PopupAnimation { get; private set; }
        public ICommand BeforeActionCommand { get; set; }
        public ICommand AfterActionCommand { get; set; }
        public void SetAnimation(IPopupAnimation popupAnimation) => PopupAnimation = popupAnimation;
    }
}