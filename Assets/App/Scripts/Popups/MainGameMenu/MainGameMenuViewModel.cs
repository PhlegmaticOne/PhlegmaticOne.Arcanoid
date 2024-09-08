using Common.Packs.Data.Models;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.MainGameMenu
{
    public class MainGameMenuViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction BackControlAction { get; set; }
        public IControlAction ContinueControlAction { get; set; }
        public IControlAction RestartControlAction { get; set; }
        public PackGameData CurrentPackGameData { get; set; }
    }
}