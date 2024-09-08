using Common.Energy;
using Common.Game.Providers;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Win.Commands
{
    public class WinMenuOnShowCommand : EmptyCommandBase
    {
        private readonly EnergyManager _energyManager;
        private readonly IPackRepository _packRepository;
        private readonly IGameDataProvider _gameDataProvider;

        public WinMenuOnShowCommand(EnergyManager energyManager,
            IPackRepository packRepository,
            IGameDataProvider gameDataProvider)
        {
            _energyManager = energyManager;
            _packRepository = packRepository;
            _gameDataProvider = gameDataProvider;
        }
        
        protected override void Execute()
        {
            var gameData = _gameDataProvider.GetGameData();
            SetNextLevelData(gameData);
            _energyManager.AddEnergy(gameData.PackGameData.PackConfiguration.WinLevelEnergy);
        }
        
        private void SetNextLevelData(GameData gameData)
        {
            var packPersistentData = gameData.PackGameData.PackPersistentData;
            var packLevels = gameData.PackLevelsData;

            if (packPersistentData.IsCurrentlyPassed())
            {
                PassCurrentPack(packPersistentData);
                OpenNextPackIfExists(gameData);
            }
            else
            {
                IncreasePassedLevels(packPersistentData, packLevels);
            }
            
            _packRepository.Save(packPersistentData);
        }

        private void IncreasePassedLevels(PackPersistentData packPersistentData, PackLevelsData packLevelsData)
        {
            packPersistentData.passedLevelsCount++;
            packPersistentData.currentLevelId = packLevelsData.levelIds[packPersistentData.passedLevelsCount];
        }

        private void PassCurrentPack(PackPersistentData packPersistentData)
        {
            packPersistentData.passedLevelsCount = packPersistentData.levelsCount;
            packPersistentData.currentLevelId = PackPersistentData.Passed;
            packPersistentData.isPassed = true;
        }

        private void OpenNextPackIfExists(GameData gameData)
        {
            var configuration = gameData.PackGameData.PackConfiguration;
            var next = _packRepository.GetNextPackConfiguration(configuration);

            if (next == null)
            {
                return;
            }

            var nextPersistentData = _packRepository.GetPersistentDataForPack(next);
            nextPersistentData.isOpened = true;
            _packRepository.Save(nextPersistentData);
        }
    }
}