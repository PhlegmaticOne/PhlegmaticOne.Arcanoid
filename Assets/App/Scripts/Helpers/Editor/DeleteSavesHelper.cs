using System.IO;
using Common.Configurations.Packs;
using Common.Data.Repositories.PersistentRepositories.Helpers;
using UnityEditor;
using UnityEngine;

namespace Helpers.Editor
{
    public static class DeleteSavesHelper
    {
        private const string PacksConfigurationAssetPath = "App/MainGame/Packs/PackCollectionConfiguration";
        
        [MenuItem ("Tools/Custom/Delete saves and mark to update all packs")]
        public static void DeleteSavesAndMarkToUpdatePacks()
        {
            var packsConfiguration = Resources.Load<PacksConfiguration>(PacksConfigurationAssetPath);
            
            var persistentDirectoryPath = PersistentRepositoriesHelper
                .GetPathToPersistentDirectory(packsConfiguration.PacksFileAttributes);
            
            var persistentDirectory = new DirectoryInfo(persistentDirectoryPath);

            foreach (var file in persistentDirectory.GetFiles())
            {
                file.Delete(); 
            }
            
            packsConfiguration.MarkToUpdateAllPacks();
            EditorUtility.SetDirty(packsConfiguration);
            AssetDatabase.SaveAssetIfDirty(packsConfiguration);
        }
    }
}