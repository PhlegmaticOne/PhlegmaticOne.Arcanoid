using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game.Accessors;
using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly IObjectAccessor<GameData> _gameDataAccessor;
        private readonly ILevelRepository _levelRepository;
        private readonly IObjectAccessor<LevelData> _levelDataAccessor;
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;

        public StartGameCommand(IObjectAccessor<GameData> gameDataAccessor,
            ILevelRepository levelRepository,
            IObjectAccessor<LevelData> levelDataAccessor,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _gameDataAccessor = gameDataAccessor;
            _levelRepository = levelRepository;
            _levelDataAccessor = levelDataAccessor;
            _mainGame = mainGame;
        }
        
        public void Execute()
        {
            var gameData = _gameDataAccessor.Get();
            var levelData = _levelRepository.GetLevelData(gameData.PackLevelCollection, gameData.LevelPreviewData);
            _levelDataAccessor.Set(levelData);
            _mainGame.StartGame(new MainGameData(levelData));
        }
    }
}