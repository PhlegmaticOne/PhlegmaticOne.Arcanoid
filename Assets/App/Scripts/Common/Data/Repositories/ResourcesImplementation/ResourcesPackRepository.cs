using System.Collections.Generic;
using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation.Base;
using UnityEngine;

namespace Common.Data.Repositories.ResourcesImplementation
{
    public class ResourcesPackRepository : ResourcesRepositoryBase, IPackRepository
    {
        private readonly PackCollectionConfiguration _packCollectionConfiguration;

        public ResourcesPackRepository(PackCollectionConfiguration packCollectionConfiguration)
        {
            _packCollectionConfiguration = packCollectionConfiguration;
            DefaultPackConfiguration = packCollectionConfiguration.DefaultPackConfiguration;
        }

        public bool PacksInitialized => _packCollectionConfiguration.PacksInitialized;
        public DefaultPackConfiguration DefaultPackConfiguration { get; }

        public IEnumerable<PackConfiguration> GetAll()
        {
            foreach (var directoryPath in GetSubFolders(_packCollectionConfiguration.PackCollectionSourcePath))
            {
                yield return LoadFirstAssetByFilter<PackConfiguration>(AssetTypeName<PackConfiguration>(), directoryPath);
            }
        }

        public PackLevelCollection GetLevels(string packName)
        {
            var packDirectoryPath = Combine(_packCollectionConfiguration.PackCollectionSourcePath, packName);
            var levelCollection = 
                LoadFirstAssetByFilter<PackLevelCollection>(AssetTypeName<PackLevelCollection>(), packDirectoryPath);

            if (levelCollection.IsLevelsInitialized == false)
            {
                InitializeLevelCollection(levelCollection, packName, packDirectoryPath);
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

        public void Save() => SaveAssets();

        private void InitializeLevelCollection(PackLevelCollection levelCollection, string packName, string packPath)
        {
            var levelsDirectoryPath = Combine(packPath, _packCollectionConfiguration.LevelsSubfolderName);
            var matches = GetAssetNamesInDirectory<TextAsset>(levelsDirectoryPath);
            var levelPreviews = matches.Select(x => new LevelPreviewData(int.Parse(x), false));
            levelCollection.Initialize(levelPreviews, packName);
            SaveToAssets(levelCollection);
            Save();
        }
    }
}