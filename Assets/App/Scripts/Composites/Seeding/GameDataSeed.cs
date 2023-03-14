using System;
using Common.Bag;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Libs.Services;

namespace Composites.Seeding
{
    public static class GameDataSeed
    {
        public static void TrySeedGameData()
        {
            var serviceProvider = ServiceProviderAccessor.Global;
            var objectBag = serviceProvider.GetRequiredService<IObjectBag>();
            
            var gameData = objectBag.Get<GameData>();
            
            if (gameData != null)
            {
                return;
            }
            
            var packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            var defaultPackConfiguration = packRepository.DefaultPackConfiguration;
            var defaultPack = defaultPackConfiguration.DefaultPack;

            var packPersistentData = packRepository.GetPersistentDataForPack(defaultPack);
            var packLevels = packRepository.GetLevelsForPack(packPersistentData);
            var levelId = defaultPackConfiguration.DefaultLevelId;
            var levelIndex = packLevels.GetIndexOfLevel(levelId);
            
            packPersistentData.passedLevelsCount = levelIndex == -1 ? 0 : levelIndex;
            packPersistentData.currentLevelId = levelIndex == -1 ? packLevels.levelIds[0] : packLevels.levelIds[levelIndex];
            
            gameData = new GameData(new PackGameData(defaultPack, packPersistentData), packLevels);
            objectBag.Set(gameData);
        }
    }
}