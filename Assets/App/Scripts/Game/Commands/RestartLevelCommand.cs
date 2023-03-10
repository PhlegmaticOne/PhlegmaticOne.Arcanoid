using Common.Data.Models;
using Game.Accessors;
using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class RestartLevelCommand : ICommand
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;
        private readonly IObjectAccessor<LevelData> _levelDataAccessor;

        public RestartLevelCommand(IGame<MainGameData, MainGameEvents> mainGame,
            IObjectAccessor<LevelData> levelDataAccessor)
        {
            _mainGame = mainGame;
            _levelDataAccessor = levelDataAccessor;
        }
        
        public void Execute()
        {
            var currentLevelData = _levelDataAccessor.Get();
            _mainGame.Stop();
            _mainGame.StartGame(new MainGameData(currentLevelData));
            _mainGame.Unpause();
        }
    }
}