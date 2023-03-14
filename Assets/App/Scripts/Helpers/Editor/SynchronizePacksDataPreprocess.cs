using Common.Configurations.Packs;
using Common.Data.Repositories.PersistentRepositories.Helpers;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Helpers.Editor
{
    public class SynchronizePacksDataPreprocess : IPreprocessBuildWithReport
    {
        private const string PacksConfigurationAssetPath = "App/MainGame/Packs/PackCollectionConfiguration";
        
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var packsConfiguration = Resources.Load<PacksConfiguration>(PacksConfigurationAssetPath);
            var packsInitializationHelper = new PersistentPackRepositoryInitializer(packsConfiguration);
            packsInitializationHelper.ForceUpdatePacks(packsConfiguration.RegisteredPackConfigurations);
        }
    }
}