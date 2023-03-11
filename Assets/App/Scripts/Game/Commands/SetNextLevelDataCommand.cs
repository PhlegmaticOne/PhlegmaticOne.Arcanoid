using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game.Accessors;
using Game.Base;
using Game.Commands.Base;

namespace Game.Commands
{
    public class SetNextLevelDataCommand : ICommand
    {
        private readonly IObjectAccessor<GameData> _gameDataAccessor;
        private readonly IPackRepository _packRepository;

        public SetNextLevelDataCommand(
            IObjectAccessor<GameData> gameDataAccessor,
            IPackRepository packRepository)
        {
            _gameDataAccessor = gameDataAccessor;
            _packRepository = packRepository;
        }
        
        public void Execute()
        {
            var gameData = _gameDataAccessor.Get();
            var passedLevelId = gameData.LevelPreviewData.LevelId;
            var packConfiguration = gameData.PackConfiguration;
            var packLevels = gameData.PackLevelCollection;
            
            packLevels.PassLevel(passedLevelId);
            packLevels.GetNextLevel(passedLevelId);
            packConfiguration.IncreasePassedLevelsCount();
            _packRepository.Save(packConfiguration);
            _packRepository.Save(packLevels);
            _packRepository.Save();
            
            var nextLevel = gameData.PackLevelCollection.GetNextLevel(passedLevelId);
            gameData.SetNewLevel(nextLevel);
        }
    }
}