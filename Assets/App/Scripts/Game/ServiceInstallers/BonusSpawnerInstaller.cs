using Game.GameEntities.Bonuses.Configurations;
using Game.GameEntities.Bonuses.Spawners;
using Game.GameEntities.Bonuses.Spawners.Configurations;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BonusSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BonusSpawnSystemConfiguration _bonusSpawnSystemConfiguration;
        [SerializeField] private Transform _spawnTransform;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBonusSpawner>(s =>
            {
                var global = ServiceProviderAccessor.Global;
                return new BonusSpawner(global.GetRequiredService<IPoolProvider>(),
                    _bonusSpawnSystemConfiguration, _spawnTransform);
            });
        }
    }
}