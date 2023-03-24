using Game.GameEntities.Bullets;
using Game.GameEntities.Bullets.Spawner;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BulletSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BehaviorObjectInstaller<Bullet> _bulletObjectInstaller;
        [SerializeField] private Transform _spawnTransform;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBulletSpawner>(x =>
            {
                var poolProvider = ServiceProviderAccessor.Global.GetRequiredService<IPoolProvider>();
                return new BulletSpawner(poolProvider, _bulletObjectInstaller, _spawnTransform);
            });
        }
    }
}