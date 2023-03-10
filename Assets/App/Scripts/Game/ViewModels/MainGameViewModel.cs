using Game.Commands.Base;

namespace Game.ViewModels
{
    public class MainGameViewModel
    {
        public ICommand StartCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public MainMenuViewModel MainMenuViewModel { get; set; }
    }
}