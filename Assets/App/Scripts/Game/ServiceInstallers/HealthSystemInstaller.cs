using Game.Logic.Systems.Health;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class HealthSystemInstaller : ServiceInstaller
    {
        [SerializeField] private HealthSystem _healthSystem;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_healthSystem);
        }
    }
}