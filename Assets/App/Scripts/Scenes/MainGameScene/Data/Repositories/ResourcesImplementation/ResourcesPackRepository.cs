using System.Collections.Generic;
using System.Linq;
using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data.Repositories.Base;
using Scenes.MainGameScene.Data.Repositories.ResourcesImplementation.Base;
using UnityEngine;

namespace Scenes.MainGameScene.Data.Repositories.ResourcesImplementation
{
    public class ResourcesPackRepository : ResourcesRepositoryBase, IPackRepository
    {
        private const string NumberAtTheEndOfStringRegex = "\\d+$";
        private readonly PackCollectionConfiguration _packCollectionConfiguration;

        public ResourcesPackRepository(PackCollectionConfiguration packCollectionConfiguration) => 
            _packCollectionConfiguration = packCollectionConfiguration;

        public bool PacksInitialized => _packCollectionConfiguration.PacksInitialized;

        public IEnumerable<PackConfiguration> GetAll()
        {
            foreach (var directoryPath in GetSubFolders(_packCollectionConfiguration.PackCollectionSourcePath))
            {
                yield return LoadFirstAssetByFilter<PackConfiguration>(AssetTypeName<PackConfiguration>(), directoryPath);
            }
        }

        public PackLevelCollection GetLevels(string packName, bool resetIfInitialized = false)
        {
            var packDirectoryPath = Combine(_packCollectionConfiguration.PackCollectionSourcePath, packName);
            var levelCollection = 
                LoadFirstAssetByFilter<PackLevelCollection>(AssetTypeName<PackLevelCollection>(), packDirectoryPath);

            if (levelCollection.IsInitialized == false || (levelCollection.IsInitialized && resetIfInitialized))
            {
                InitializeLevelCollection(levelCollection, packDirectoryPath);
            }

            return levelCollection;
        }

        public int GetLevelsCount(string packName)
        {
            var levelsDirectoryPath = Combine(
                Combine(_packCollectionConfiguration.PackCollectionSourcePath, packName),
                _packCollectionConfiguration.LevelsSubfolderName);
            return AssetsCountByFilter(AssetTypeName<TextAsset>(), levelsDirectoryPath);
        }

        public void Save(PackLevelCollection packLevelCollection) => SaveToAssets(packLevelCollection);
        public void Save(PackConfiguration packConfiguration) => SaveToAssets(packConfiguration);
        public void MarkAsInitialized()
        {
            _packCollectionConfiguration.MarkPacksInitialized();
            SaveToAssets(_packCollectionConfiguration);
        }

        private void InitializeLevelCollection(PackLevelCollection levelCollection, string packPath)
        {
            var levelsDirectoryPath = Combine(packPath, _packCollectionConfiguration.LevelsSubfolderName);
            var matches = GetMatchesInAssetNames<TextAsset>(NumberAtTheEndOfStringRegex, levelsDirectoryPath);
            var levelPreviews = matches.Select(x => new LevelPreviewData(int.Parse(x), false));
            levelCollection.Initialize(levelPreviews);
        }
    }
}