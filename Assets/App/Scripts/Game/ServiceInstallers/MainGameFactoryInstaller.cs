using Game.Base;
using Game.Field.Builder;
using Game.GameEntities.Bonuses;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Game.GameEntities.PlayerObjects.ShipObject;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.ServiceInstallers
{
    public class MainGameFactoryInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGameFactory<MainGame>>(x =>
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
                return new MainGameFactory(fieldBuilder, poolProvider, ballSpawner, inputSystem,
                    controlSystem, healthSystem, balls, bonuses, ship);
            });
        }
    }
}