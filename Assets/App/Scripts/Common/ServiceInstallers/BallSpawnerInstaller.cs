using Game.Behaviors.Installer;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class BallSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BehaviorObjectInstaller<Ball> _ballBehaviorInstaller;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBallSpawner>(s =>
                new BallSpawner(s.GetRequiredService<IPoolProvider>(), _ballBehaviorInstaller));
        }
    }
}