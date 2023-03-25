using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Scenes;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.PackChoose.Commands
{
    public class PackClickedCommand : ParameterCommandBase<PackGameData>
    {
        private readonly EnergyManager _energyManager;
        private readonly IPopupManager _popupManager;
        private readonly IObjectBag _objectBag;
        private readonly ISceneChanger _sceneChanger;
        private readonly IPackRepository _packRepository;

        public PackClickedCommand(EnergyManager energyManager, 
            IPopupManager popupManager,
            IObjectBag objectBag, 
            ISceneChanger sceneChanger,
            IPackRepository packRepository)
        {
            _energyManager = energyManager;
            _popupManager = popupManager;
            _objectBag = objectBag;
            _sceneChanger = sceneChanger;
            _packRepository = packRepository;
        }
        
        protected override void Execute(PackGameData parameter)
        {
            SetGameData(parameter);
            _energyManager.SpendEnergy(parameter.PackConfiguration.StartLevelEnergy);
            _sceneChanger.ChangeScene(SceneIndexes.GameScene);
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
            var gameData = new GameData(packGameData, packLevelCollection);
            _objectBag.Set(gameData);
        }

        protected override bool CanExecute(PackGameData parameter)
        {
            return _energyManager.CanSpendEnergy(parameter.PackConfiguration.StartLevelEnergy);
        }
    }
}