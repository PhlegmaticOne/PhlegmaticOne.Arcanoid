using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.ClearData
{
    public class ClearDataPopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction AcceptControlAction { get; set; }
        public IControlAction CancelControlAction { get; set; }
    }
}