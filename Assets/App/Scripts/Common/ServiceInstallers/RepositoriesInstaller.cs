using Common.Packs.Configurations;
using Common.Packs.Data.Repositories.Base;
using Common.Packs.Data.Repositories.PersistentRepositories;
using Common.Scenes;
using Libs.Popups.Base;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class RepositoriesInstaller : ServiceInstaller
    {
        [SerializeField] private PacksConfiguration _packCollectionConfiguration;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IPackRepository>(new PersistentPackRepository(_packCollectionConfiguration))
                .AddSingleton<ILevelRepository>(new PersistentLevelRepository(_packCollectionConfiguration));

            serviceCollection.AddSingleton<ISceneChanger>(x => new NewSceneChanger(x.GetRequiredService<IPopupManager>()));
        }
    }
}