using Game.Base;
using Game.Field.Builder;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Game.GameEntities.PlayerObjects.ShipObject;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;
using Game.ObjectParticles;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.Services;
using Libs.TimeActions;

namespace Game.ServiceInstallers
{
    public class MainGameInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGame<MainGameData, MainGameEvents>>(x =>
            {
                var global = ServiceProviderAccessor.Global;
                
                var ballSpawner = x.GetRequiredService<IBallSpawner>();
                var fieldBuilder = x.GetRequiredService<IFieldBuilder>();
                var inputSystem = x.GetRequiredService<IInputSystem>();
                var healthSystem = x.GetRequiredService<HealthSystem>();
                var controlSystem = x.GetRequiredService<ControlSystem>();
                var balls = x.GetRequiredService<BallsOnField>();
                var bonuses = x.GetRequiredService<BonusesOnField>();
                var ship = x.GetRequiredService<Ship>();
                var poolProvider = global.GetRequiredService<IPoolProvider>();
                var timeActionsManager = x.GetRequiredService<TimeActionsManager>();
                var captiveBallsSystem = x.GetRequiredService<CaptiveBallsSystem>();
                var particleManager = x.GetRequiredService<ParticleManager>();
                
                controlSystem.Initialize(inputSystem, ship);
                return new MainGame(poolProvider,
                    fieldBuilder,
                    timeActionsManager,
                    healthSystem,
                    balls,
                    bonuses,
                    controlSystem,
                    captiveBallsSystem,
                    particleManager,
                    ballSpawner,
                    ship);
            });
        }
    }
}