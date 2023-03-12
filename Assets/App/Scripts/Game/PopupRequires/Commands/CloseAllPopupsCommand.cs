using Game.PopupRequires.Commands.Base;
using Libs.Popups.Base;

namespace Game.PopupRequires.Commands
{
    public class CloseAllPopupsCommand : ICommand
    {
        private readonly IPopupManager _popupManager;

        public CloseAllPopupsCommand(IPopupManager popupManager) => _popupManager = popupManager;

        public void Execute() => _popupManager.CloseAllPopupsInstant();
    }
}