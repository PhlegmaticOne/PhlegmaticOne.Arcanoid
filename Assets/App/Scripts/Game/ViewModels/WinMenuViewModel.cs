using Game.Commands.Base;

namespace Game.ViewModels
{
    public class WinMenuViewModel
    {
        public ICommand OnShowingCommand { get; set; }
        public ICommand OnNextButtonClickCommand { get; set; }
        public ICommand OnClosedCommand { get; set; }
        public ICommand OnLastClosedCommand { get; set; }
    }
}