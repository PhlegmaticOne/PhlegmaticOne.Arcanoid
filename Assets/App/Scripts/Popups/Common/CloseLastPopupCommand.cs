using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class CloseLastPopupCommand : ICommand
    {
        private readonly IPopupManager _popupManager;

        public CloseLastPopupCommand(IPopupManager popupManager) => _popupManager = popupManager;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            _popupManager.CloseLastPopup();
        }
    }
}