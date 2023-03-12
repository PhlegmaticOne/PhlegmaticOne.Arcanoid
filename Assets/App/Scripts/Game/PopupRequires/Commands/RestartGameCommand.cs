using Common.Bag;
using Common.Data.Models;
using Game.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class RestartMainGameCommand : ICommand
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;
        private readonly IObjectBag _objectBag;

        public RestartMainGameCommand(IGame<MainGameData, MainGameEvents> mainGame, IObjectBag objectBag)
        {
            _mainGame = mainGame;
            _objectBag = objectBag;
        }
        
        public void Execute()
        {
            _mainGame.Stop();
            var currentLevelData = _objectBag.Get<LevelData>();
            _mainGame.StartGame(new MainGameData(currentLevelData));
        }
    }
}