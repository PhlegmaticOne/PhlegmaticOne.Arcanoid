using System.IO;
using Common.Packs.Configurations;
using UnityEngine;

namespace Common.Packs.Data.Repositories.PersistentRepositories.Helpers
{
    public static class PersistentRepositoriesHelper
    {
        private const string Format = ".json";
        private const string ResourcesDirectoryName = "Resources";
        
        internal static void SaveToTextFile<T>(T entity, string directoryPath, string fileName)
        {
            var path = Combine(directoryPath, fileName + Format);
            
            var packDataJson = JsonUtility.ToJson(entity);
            
            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            File.WriteAllText(path, packDataJson);
        }

        internal static T LoadFromTextFile<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        
        internal static T LoadFromResources<T>(string filePath)
        {
            var json = Resources.Load<TextAsset>(filePath);
            return JsonUtility.FromJson<T>(json.text);
        }
        

        internal static void TryCreatePersistentDirectory(PacksFileAttributes packsFileAttributes)
        {
            var pathToPersistentDirectory = GetPathToPersistentDirectory(packsFileAttributes);
            
            if (Directory.Exists(pathToPersistentDirectory) == false)
            {
                Directory.CreateDirectory(pathToPersistentDirectory);
            }
        }

        internal static string FormatDataFileName(string fileName, PacksFileAttributes packsFileAttributes) => 
            fileName + packsFileAttributes.PackDataFilePostfix;

        internal static string FormatPreviewFileName(string fileName, PacksFileAttributes packsFileAttributes) => 
            fileName + packsFileAttributes.PackPreviewFilePostfix;

        internal static string GetPackPathDirectory(PackConfiguration packConfiguration, 
            PacksFileAttributes packsFileAttributes) => 
            Combine(Application.dataPath, ResourcesDirectoryName,
                packsFileAttributes.PacksInResourcesDirectoryPath, packConfiguration.Name);

        internal static string GetPathToPersistentDataFile(PackConfiguration packConfiguration, PacksFileAttributes packsFileAttributes) => 
            Combine(GetPathToPersistentDirectory(packsFileAttributes), packConfiguration.Name + Format);

        public static string GetPathToPersistentDirectory(PacksFileAttributes packsFileAttributes) => 
            Combine(Application.persistentDataPath, packsFileAttributes.PacksInPersistentDirectoryPath);

        internal static string Combine(params string[] values) => string.Join("/", values);
    }
}