using System.Linq;
using Common.Bag;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
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
            var packPersistentData = gameData.PackGameData.PackPersistentData;
            
            var passedLevelId = packPersistentData.currentLevelId;
            var packLevels = gameData.PackLevelsData;

            if (passedLevelId == packLevels.levelIds.Last())
            {
                packPersistentData.passedLevelsCount = packPersistentData.levelsCount;
                packPersistentData.currentLevelId = PackPersistentData.Passed;
            }
            else
            {
                packPersistentData.passedLevelsCount++;
                packPersistentData.currentLevelId = packLevels.levelIds[packPersistentData.passedLevelsCount];
            }
            
            _packRepository.Save(packPersistentData);
        }
    }
}