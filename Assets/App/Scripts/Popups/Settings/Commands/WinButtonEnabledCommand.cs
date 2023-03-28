using Common.WinButton;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Settings.Commands
{
    public class WinButtonEnabledCommand : ParameterCommandBase<bool>
    {
        private readonly IWinButtonEnabledProvider _winButtonEnabledProvider;

        public WinButtonEnabledCommand(IWinButtonEnabledProvider winButtonEnabledProvider) => 
            _winButtonEnabledProvider = winButtonEnabledProvider;
        protected override void Execute(bool parameter) => 
            _winButtonEnabledProvider.ChangeEnabled(parameter);
    }
}