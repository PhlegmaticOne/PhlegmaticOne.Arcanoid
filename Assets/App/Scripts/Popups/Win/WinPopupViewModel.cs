using Common.Packs.Data.Models;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;

namespace Popups.Win
{
    public class WinPopupViewModel : IPopupViewModel
    {
        public IPopupAction ShowAction { get; set; }
        public IPopupAction CloseAction { get; set; }
        public IControlAction NextControlAction { get; set; }
        public IControlAction BackControlAction { get; set; }
        public WinState WinState { get; set; }
        
        public PackGameData CurrentPackData { get; set; }
        public PackGameData NextPackData { get; set; }
        
    }

    public enum WinState
    {
        NextLevelInCurrentPack,
        PackPassedFirstTime,
        PackPassedMultipleTime,
        AllPacksPassed
    }
}