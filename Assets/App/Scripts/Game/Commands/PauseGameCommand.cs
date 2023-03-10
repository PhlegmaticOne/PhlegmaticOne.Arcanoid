using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class PauseGameCommand : ICommand
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;

        public PauseGameCommand(IGame<MainGameData, MainGameEvents> mainGame)
        {
            _mainGame = mainGame;
        }
        
        public void Execute()
        {
            _mainGame.Pause();
        }
    }
}