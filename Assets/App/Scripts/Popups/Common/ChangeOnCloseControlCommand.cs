using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class ChangeOnCloseControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly IPopupManager _popupManager;
        private readonly ICommand _commandToReplace;

        public ChangeOnCloseControlCommand(IPopupManager popupManager, ICommand commandToReplace)
        {
            _popupManager = popupManager;
            _commandToReplace = commandToReplace;
        }
        
        protected override bool CanExecute(IPopupViewModel popupViewModel) => true;

        protected override void Execute(IPopupViewModel parameter)
        {
            parameter.CloseAction.AfterActionCommand = _commandToReplace;
            _popupManager.CloseLastPopup();
        }
    }
}