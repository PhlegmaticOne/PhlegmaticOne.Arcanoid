using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BallSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BehaviorObjectInstaller<Ball> _ballBehaviorInstaller;
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private float _ballInitialSpeed;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBallSpawner>(s =>
            {
                var global = ServiceProviderAccessor.Global;
                return new BallSpawner(global.GetRequiredService<IPoolProvider>(), _ballBehaviorInstaller, _ballInitialSpeed, _spawnTransform);
            });
        }
    }
}