using Game.Behaviors.Installer;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BallSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BehaviorObjectInstaller<Ball> _ballBehaviorInstaller;
        [SerializeField] private Transform _spawnTransform;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBallSpawner>(s =>
            {
                var global = ServiceProviderAccessor.Global;
                return new BallSpawner(global.GetRequiredService<IPoolProvider>(), _ballBehaviorInstaller, _spawnTransform);
            });
        }
    }
}