using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation.Base;
using UnityEngine;

namespace Common.Data.Repositories.ResourcesImplementation
{
    public class ResourcesLevelRepository : ResourcesRepositoryBase,  ILevelRepository
    {
        private readonly PackCollectionConfiguration _packCollectionConfiguration;

        public ResourcesLevelRepository(PackCollectionConfiguration packCollectionConfiguration) => 
            _packCollectionConfiguration = packCollectionConfiguration;

        public LevelData GetLevelData(PackLevelCollection packLevelCollection, LevelPreviewData levelPreviewData)
        {
            var packName = packLevelCollection.PackName;
            var levelsDirectoryPath = Combine(
                Combine(_packCollectionConfiguration.PackCollectionSourcePath, packName),
                _packCollectionConfiguration.LevelsSubfolderName);

            var levelTextAsset = LoadFirstAssetByFilter<TextAsset>(packName + "_" + levelPreviewData.LevelId, levelsDirectoryPath);
            return JsonUtility.FromJson<LevelData>(levelTextAsset.text);
        }
    }
}