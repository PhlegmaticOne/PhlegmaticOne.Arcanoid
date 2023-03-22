using Libs.Popups;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class SpawnPopupCommand<T> : ICommand where T : Popup
    {
        private readonly IPopupManager _popupManager;
        public SpawnPopupCommand(IPopupManager popupManager) => _popupManager = popupManager;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _popupManager.SpawnPopup<T>();
    }
}