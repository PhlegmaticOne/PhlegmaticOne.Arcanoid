using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.Start
{
    public class StartPopupViewModel : IPopupViewModel
    {
        public IControlAction SettingsControlAction { get; set; }
        public IControlAction PlayControlAction { get; set; }
        public IControlAction ExitControlAction { get; set; }
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
    }
}