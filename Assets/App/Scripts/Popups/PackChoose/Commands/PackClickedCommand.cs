using Common.Energy;
using Common.Game.Providers;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
using Common.Scenes;
using Libs.Popups.ViewModels.Commands;

namespace Common.Packs.Views.Commands
{
    public class PackClickedCommand : ParameterCommandBase<PackGameData>
    {
        private readonly EnergyManager _energyManager;
        private readonly IGameDataProvider _gameDataProvider;
        private readonly ISceneChanger _sceneChanger;
        private readonly IPackRepository _packRepository;

        public PackClickedCommand(EnergyManager energyManager, 
            IGameDataProvider gameDataProvider, 
            ISceneChanger sceneChanger,
            IPackRepository packRepository)
        {
            _energyManager = energyManager;
            _gameDataProvider = gameDataProvider;
            _sceneChanger = sceneChanger;
            _packRepository = packRepository;
        }
        
        protected override bool CanExecute(PackGameData parameter)
        {
            return _energyManager.CanSpendEnergy(parameter.PackConfiguration.StartLevelEnergy);
        }
        
        protected override void Execute(PackGameData parameter)
        {
            SetGameData(parameter);
            _energyManager.SpendEnergy(parameter.PackConfiguration.StartLevelEnergy);
            _sceneChanger.ChangeScene(SceneNames.Game);
        }

        private void SetGameData(PackGameData packGameData)
        {
            var packPersistentData = packGameData.PackPersistentData;
            var packLevelCollection = _packRepository.GetLevelsForPack(packPersistentData);
            var currentLevelIdIndex = packPersistentData.passedLevelsCount;
            
            if (packPersistentData.passedLevelsCount == packPersistentData.levelsCount)
            {
                packPersistentData.passedLevelsCount = 0;
                _packRepository.Save(packPersistentData);
                currentLevelIdIndex = 0;
            }
            
            var currentLevel = packLevelCollection.levelIds[currentLevelIdIndex];
            packPersistentData.currentLevelId = currentLevel;
            _gameDataProvider.Update(new GameData(packGameData, packLevelCollection));
        }
    }
}