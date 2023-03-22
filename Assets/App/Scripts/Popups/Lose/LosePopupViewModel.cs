using Common.Packs.Data.Models;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.Lose
{
    public class LosePopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction BackControlAction { get; set; }
        public IControlAction BuyLifeControlAction { get; set; }
        public IControlAction RestartControlAction { get; set; }
        public PackGameData CurrentPack { get; set; }
    }
}