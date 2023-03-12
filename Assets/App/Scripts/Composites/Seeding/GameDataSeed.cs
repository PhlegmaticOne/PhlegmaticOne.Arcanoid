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
            
            if (defaultPack.IsPassed)
            {
                defaultPack.ResetPassedLevelsCount();
            }
            
            var levelCollection = packRepository.GetLevels(defaultPackConfiguration.DefaultPack.Name);
            var levelId = defaultPackConfiguration.DefaultLevelIndex;
            gameData = new GameData(defaultPackConfiguration.DefaultPack, levelCollection, new LevelPreviewData(levelId, false));
            objectBag.Set(gameData);
        }
    }
}