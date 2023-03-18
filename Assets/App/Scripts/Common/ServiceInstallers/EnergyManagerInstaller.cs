using Common.Energy;
using Common.Energy.Configurations;
using Common.Energy.Repositories;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class EnergyManagerInstaller : ServiceInstaller
    {
        [SerializeField] private EnergyManager _energyManager;
        [SerializeField] private EnergyConfiguration _energyConfiguration;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            var energyManager = Instantiate(_energyManager);
            energyManager.Initialize(new PersistentEnergyRepository(_energyConfiguration));
            serviceCollection.AddSingleton(energyManager);
        }
    }
}