using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand RestartCommand { get; set; }
        public ICommand ContinueCommand { get; set; }
        public ICommand BackToPackMenuCommand { get; set; }
    }
}