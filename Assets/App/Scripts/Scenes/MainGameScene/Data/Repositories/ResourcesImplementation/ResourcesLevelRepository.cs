using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data.Repositories.Base;
using Scenes.MainGameScene.Data.Repositories.ResourcesImplementation.Base;
using UnityEngine;

namespace Scenes.MainGameScene.Data.Repositories.ResourcesImplementation
{
    public class ResourcesLevelRepository : ResourcesRepositoryBase,  ILevelRepository
    {
        private readonly PackCollectionConfiguration _packCollectionConfiguration;

        public ResourcesLevelRepository(PackCollectionConfiguration packCollectionConfiguration) => 
            _packCollectionConfiguration = packCollectionConfiguration;

        public LevelData GetLevelData(int levelId, string packName)
        {
            var levelsDirectoryPath = Combine(
                Combine(_packCollectionConfiguration.PackCollectionSourcePath, packName),
                _packCollectionConfiguration.LevelsSubfolderName);

            var levelTextAsset = LoadFirstAssetByFilter<TextAsset>(packName + "_" + levelId, levelsDirectoryPath);
            return JsonUtility.FromJson<LevelData>(levelTextAsset.text);
        }
    }
}