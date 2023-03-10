using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Common.Data.Repositories.ResourcesImplementation.Base
{
    public abstract class ResourcesRepositoryBase
    {
        protected static string[] GetSubFolders(string path) => AssetDatabase.GetSubFolders(path);

        protected static IEnumerable<string> GetAssetNamesInDirectory<TAsset>(string directoryName) where TAsset : Object
        {
            foreach (var asset in FindAssets(AssetTypeName<TAsset>(), directoryName))
            {
                var assetPath = ToAssetPath(asset);
                yield return Path.GetFileNameWithoutExtension(assetPath);
            }
        }

        protected static TAsset LoadFirstAssetByFilter<TAsset>(string filter, string directoryPath)
            where TAsset : Object
        {
            var packConfigurationAsset = FindAssets(filter, directoryPath).First();
            var assetPath = ToAssetPath(packConfigurationAsset);
            return AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
        }

        protected static void SaveToAssets<TAsset>(TAsset asset) where TAsset : Object
        {
            EditorUtility.SetDirty(asset);
            //AssetDatabase.SaveAssetIfDirty(asset);
        }

        protected static void SaveAssets() => AssetDatabase.SaveAssets();

        protected static int AssetsCountByFilter(string filter, string directoryPath) =>
            FindAssets(filter, directoryPath).Length;
        
        protected static string AssetTypeName<TAsset>() => "t:" + typeof(TAsset).Name;
        
        protected static string Combine(string s1, string s2) => s1 + "/" + s2;

        private static string ToAssetPath(string assetGuid) => AssetDatabase.GUIDToAssetPath(assetGuid);

        private static string[] FindAssets(string filter, string directoryPath) =>
            AssetDatabase.FindAssets(filter, new[] { directoryPath });
    }
}