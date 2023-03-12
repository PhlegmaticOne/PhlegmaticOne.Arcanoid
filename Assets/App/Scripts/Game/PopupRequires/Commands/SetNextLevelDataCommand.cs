using Common.Bag;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class SetNextLevelDataCommand : ICommand
    {
        private readonly IObjectBag _objectBag;
        private readonly IPackRepository _packRepository;

        public SetNextLevelDataCommand(IObjectBag objectBag, IPackRepository packRepository)
        {
            _objectBag = objectBag;
            _packRepository = packRepository;
        }
        
        public void Execute()
        {
            var gameData = _objectBag.Get<GameData>();
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