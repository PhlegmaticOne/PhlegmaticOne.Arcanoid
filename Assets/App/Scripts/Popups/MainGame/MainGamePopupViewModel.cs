using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.MainGame
{
    public class MainGamePopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction MenuControlAction { get; set; }
        public IControlAction WinControlAction { get; set; }
    }
}