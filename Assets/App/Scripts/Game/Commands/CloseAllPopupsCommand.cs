using Game.Commands.Base;
using Libs.Popups.Base;

namespace Game.Commands
{
    public class CloseAllPopupsCommand : ICommand
    {
        private readonly IPopupManager _popupManager;

        public CloseAllPopupsCommand(IPopupManager popupManager) => _popupManager = popupManager;

        public void Execute() => _popupManager.CloseAllPopupsInstant();
    }
}