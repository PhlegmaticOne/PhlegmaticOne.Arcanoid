using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Packs.Data.Repositories.PersistentRepositories.Helpers;

namespace Common.Packs.Data.Repositories.PersistentRepositories
{
    public class PersistentLevelRepository : ILevelRepository
    {
        private readonly PacksFileAttributes _packsFileAttributes;

        public PersistentLevelRepository(PacksConfiguration packCollectionConfiguration) => 
            _packsFileAttributes = packCollectionConfiguration.PacksFileAttributes;
        

        public LevelData GetLevelData(PackPersistentData packPersistentData)
        {
            var levelFilePath = BuildPathToLevelData(packPersistentData);
            return PersistentRepositoriesHelper.LoadFromResources<LevelData>(levelFilePath);
        }

        private string BuildPathToLevelData(PackPersistentData packPersistentData)
        {
            return PersistentRepositoriesHelper.Combine(
                _packsFileAttributes.PacksInResourcesDirectoryPath,
                packPersistentData.name,
                _packsFileAttributes.LevelsSubfolderName,
                packPersistentData.currentLevelId.ToString());
        }
    }
}