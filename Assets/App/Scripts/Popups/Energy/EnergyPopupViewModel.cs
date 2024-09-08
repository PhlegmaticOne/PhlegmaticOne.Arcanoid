using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.Energy
{
    public class EnergyPopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction OkControlAction { get; set; }
    }
}