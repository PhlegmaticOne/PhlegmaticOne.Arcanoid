using System.Collections.Generic;
using System.IO;
using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Packs.Data.Repositories.PersistentRepositories.Helpers;
using UnityEditor;

namespace Common.Packs.Data.Repositories.PersistentRepositories
{
    public class PersistentPackRepository : IPackRepository
    {
        private readonly PacksConfiguration _packsConfiguration;
        private readonly PacksFileAttributes _packsFileAttributes;

        public PersistentPackRepository(PacksConfiguration packsConfiguration)
        {
            _packsConfiguration = packsConfiguration;
            _packsFileAttributes = packsConfiguration.PacksFileAttributes;
            DefaultPackConfiguration = packsConfiguration.DefaultPackConfiguration;
            
            #if UNITY_EDITOR
                var repositoryInitializer = new PersistentPackRepositoryInitializer(packsConfiguration);
                repositoryInitializer.TryUpdatePacksData();
            #endif
        }
        
        public DefaultPackConfiguration DefaultPackConfiguration { get; }
        
        public IEnumerable<PackGameData> GetAll()
        {
            var result = new List<PackGameData>();
            
            foreach (var packConfiguration in _packsConfiguration.RegisteredPackConfigurations)
            {
                #if UNITY_EDITOR
                    if (_packsConfiguration.IsUpdatePacksInEditor &&
                        (_packsConfiguration.IsUpdateAllPacks || _packsConfiguration.PackConfigurations[packConfiguration]))
                    {
                        var editorPersistentData = CreatePackPersistentData(packConfiguration);
                        result.Add(new PackGameData(packConfiguration, editorPersistentData));
                        continue;
                    }
                #endif
                
                var persistentData = GetPersistentDataForPack(packConfiguration);
                result.Add(new PackGameData(packConfiguration, persistentData));
            }
            
            #if UNITY_EDITOR
                if (_packsConfiguration.IsInitialized == false)
                {
                    _packsConfiguration.MarkAsInitialized();
                    EditorUtility.SetDirty(_packsConfiguration);
                    AssetDatabase.SaveAssetIfDirty(_packsConfiguration);
                }
            #endif

            return result;
        }


        public PackPersistentData GetPersistentDataForPack(PackConfiguration packConfiguration)
        {
            var persistentDataPath = PersistentRepositoriesHelper
                .GetPathToPersistentDataFile(packConfiguration, _packsFileAttributes);
            
            return File.Exists(persistentDataPath) == false ? 
                CreatePackPersistentData(packConfiguration) : 
                PersistentRepositoriesHelper.LoadFromTextFile<PackPersistentData>(persistentDataPath);
        }

        public PackLevelsData GetLevelsForPack(PackPersistentData packPersistentData)
        {
            var dataPath = GetRelativePathToDataDirectory(packPersistentData);
            var fileName = PersistentRepositoriesHelper
                .FormatDataFileName(packPersistentData.name, _packsFileAttributes);
            var combined = PersistentRepositoriesHelper.Combine(dataPath, fileName);
            return PersistentRepositoriesHelper.LoadFromResources<PackLevelsData>(combined);
        }

        public void Save(PackPersistentData packPersistentData)
        {
            var persistentPath = PersistentRepositoriesHelper.GetPathToPersistentDirectory(_packsFileAttributes);
            PersistentRepositoriesHelper.SaveToTextFile(packPersistentData, persistentPath, packPersistentData.name);
        }

        private string GetRelativePathToDataDirectory(PackPersistentData packPersistentData)
        {
            return PersistentRepositoriesHelper.Combine(_packsFileAttributes.PacksInResourcesDirectoryPath, 
                packPersistentData.name, _packsFileAttributes.DataSubfolderName);
        }
        
        private PackPersistentData CreatePackPersistentData(PackConfiguration packConfiguration)
        {
            var previewData = GetPackPreviewData(packConfiguration);
            
            var persistentData = new PackPersistentData
            {
                name = previewData.name,
                levelsCount = previewData.levelsCount,
                currentLevelId = previewData.startLevelId,
                passedLevelsCount = 0
            };

            Save(persistentData);
            
            return persistentData;
        }
        
        private PackPreviewData GetPackPreviewData(PackConfiguration packConfiguration)
        {
            var dataPath = GetRelativePathToDataDirectory(new PackPersistentData{name=packConfiguration.Name});
            var fileName = PersistentRepositoriesHelper
                .FormatPreviewFileName(packConfiguration.Name, _packsFileAttributes);
            
            return PersistentRepositoriesHelper.LoadFromResources<PackPreviewData>(
                PersistentRepositoriesHelper.Combine(dataPath, fileName));
        }
    }
}