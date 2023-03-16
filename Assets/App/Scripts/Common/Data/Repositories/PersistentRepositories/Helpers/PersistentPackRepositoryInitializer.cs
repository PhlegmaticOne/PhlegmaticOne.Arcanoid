using System.Collections.Generic;
using System.IO;
using Common.Configurations.Packs;
using Common.Data.Models;
using UnityEditor;
using UnityEngine;

namespace Common.Data.Repositories.PersistentRepositories.Helpers
{
    public class PersistentPackRepositoryInitializer
    {
        private readonly PacksConfiguration _packsConfiguration;
        private readonly PacksFileAttributes _packsFileAttributes;
        
        public PersistentPackRepositoryInitializer(PacksConfiguration packsConfiguration)
        {
            _packsConfiguration = packsConfiguration;
            _packsFileAttributes = packsConfiguration.PacksFileAttributes;
            PersistentRepositoriesHelper.TryCreatePersistentDirectory(_packsFileAttributes);
        }
        
        internal void TryUpdatePacksData()
        {
            if (_packsConfiguration.IsUpdatePacksInEditor == false)
            {
                return;
            }
            
            ForceUpdatePacks(GetPacksToUpdate());
        }

        public void ForceUpdatePacks(IEnumerable<PackConfiguration> packConfigurations)
        {
            foreach (var packConfiguration in packConfigurations)
            {
                var packDirectoryPath = PersistentRepositoriesHelper
                    .GetPackPathDirectory(packConfiguration, _packsFileAttributes);
                
                var dataDirectoryPath = PersistentRepositoriesHelper
                    .Combine(packDirectoryPath, _packsFileAttributes.DataSubfolderName);
                
                var packLevelsData = CreatePackLevelsData(packDirectoryPath);
                
                var packPreviewData = new PackPreviewData
                {
                    name = packConfiguration.Name,
                    levelsCount = packLevelsData.levelIds.Length,
                    startLevelId = packLevelsData.levelIds[0]
                };

                PersistentRepositoriesHelper.SaveToTextFile(packLevelsData, dataDirectoryPath,
                    PersistentRepositoriesHelper.FormatDataFileName(packConfiguration.Name, _packsFileAttributes));
                
                PersistentRepositoriesHelper.SaveToTextFile(packPreviewData, dataDirectoryPath,
                    PersistentRepositoriesHelper.FormatPreviewFileName(packConfiguration.Name, _packsFileAttributes));
            }

            AssetDatabase.Refresh();
        }

        private ICollection<PackConfiguration> GetPacksToUpdate()
        {
            var packsToUpdate = new List<PackConfiguration>();

            if (_packsConfiguration.IsUpdateAllPacks)
            {
                return _packsConfiguration.RegisteredPackConfigurations;
            }
            
            foreach (var packConfiguration in _packsConfiguration.PackConfigurations)
            {
                var configuration = packConfiguration.Key;

                if (packConfiguration.Value)
                {
                    packsToUpdate.Add(configuration);
                    continue;
                }
                
                var pathToPersistentData = PersistentRepositoriesHelper
                    .GetPathToPersistentDataFile(configuration, _packsFileAttributes);
                
                if (File.Exists(pathToPersistentData) == false)
                {
                    packsToUpdate.Add(configuration);
                }
            }

            return packsToUpdate;
        }
        
        private PackLevelsData CreatePackLevelsData(string packDirectoryPath)
        {
            var levelsPath = PersistentRepositoriesHelper
                .Combine(packDirectoryPath, _packsFileAttributes.LevelsSubfolderName);
            var files = Directory.GetFiles(levelsPath);
            var levelsIds = new List<int>();

            for (var i = 0; i < files.Length; i += 2)
            {
                var fileName = Path.GetFileNameWithoutExtension(files[i]);
                levelsIds.Add(int.Parse(fileName));
            }
            
            return new PackLevelsData
            {
                levelIds = levelsIds.ToArray()
            };
        }
    }
}