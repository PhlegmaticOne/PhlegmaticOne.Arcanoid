using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.PackChoose
{
    public class PackChoosePopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction BackControlAction { get; set; }
        public IControlAction PackClickedAction { get; set; }
    }
}