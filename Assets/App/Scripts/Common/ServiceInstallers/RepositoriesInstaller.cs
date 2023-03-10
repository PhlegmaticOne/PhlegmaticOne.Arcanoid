using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Common.Data.Repositories.ResourcesImplementation;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class RepositoriesInstaller : ServiceInstaller
    {
        [SerializeField] private PackCollectionConfiguration _packCollectionConfiguration;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IPackRepository>(new ResourcesPackRepository(_packCollectionConfiguration))
                .AddSingleton<ILevelRepository>(new ResourcesLevelRepository(_packCollectionConfiguration));
        }
    }
}