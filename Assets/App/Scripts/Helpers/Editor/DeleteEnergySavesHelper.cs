using System.IO;
using Common.Energy.Configurations;
using Common.Energy.Repositories;
using UnityEditor;

namespace Helpers.Editor
{
    public static class DeleteEnergySavesHelper
    {
        private const string EnergyConfigurationAssetPath = "Assets/App/Configurations/Common/EnergyConfiguration.asset";

        [MenuItem("Tools/Custom/Delete energy saves")]
        
        public static void DeleteSaves()
        {
            var energyConfiguration = AssetDatabase.LoadAssetAtPath<EnergyConfiguration>(EnergyConfigurationAssetPath);
            var path = PersistentEnergyRepository.GetEnergySaveFilePath(energyConfiguration);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}