using Game.ObjectParticles;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class ParticleManagerInstaller : ServiceInstaller
    {
        [SerializeField] private Transform _spawnTransform;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var poolProvider = ServiceProviderAccessor.Global.GetRequiredService<IPoolProvider>();
                return new ParticleManager(poolProvider, _spawnTransform);
            });
        }
    }
}