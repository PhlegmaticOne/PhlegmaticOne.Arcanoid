using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class ContinueGameCommand : ICommand
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;

        public ContinueGameCommand(IGame<MainGameData, MainGameEvents> mainGame)
        {
            _mainGame = mainGame;
        }
        
        public void Execute()
        {
            _mainGame.Unpause();
        }
    }
}