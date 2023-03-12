using Game.Commands.Base;

namespace Game.ViewModels
{
    public class LosePopupViewModel
    {
        public ICommand RestartButtonCommand { get; set; }
        public ICommand OnShowingCommand { get; set; }
    }
}