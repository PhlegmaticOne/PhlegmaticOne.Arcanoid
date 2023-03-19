using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.ViewModels
{
    public class LosePopupViewModel
    {
        public ICommand RestartButtonCommand { get; set; }
        public ICommand BuyLifeButtonCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }
        public ICommand OnShowingCommand { get; set; }
    }
}