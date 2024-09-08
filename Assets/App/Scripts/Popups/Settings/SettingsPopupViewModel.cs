using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.Settings
{
    public class SettingsPopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction CloseControlAction { get; set; }
        public IControlAction WinButtonEnabledControlAction { get; set; }
        public IControlAction ChangeLocaleControlAction { get; set; }
        public IControlAction ClearDataControlAction { get; set; }
    }
}