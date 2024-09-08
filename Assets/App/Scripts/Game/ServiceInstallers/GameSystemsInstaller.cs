using Game.Composites;
using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class GameSystemsInstaller : ServiceInstaller
    {
        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private ControlSystem _controlSystem;
        [SerializeField] private CaptiveBallsSystem _captiveBallsSystem;

        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new GameSystems(_healthSystem, _controlSystem, _captiveBallsSystem));
            serviceCollection.AddSingleton(_healthSystem);
            serviceCollection.AddSingleton(_controlSystem);
            serviceCollection.AddSingleton(_captiveBallsSystem);
        }
    }
}