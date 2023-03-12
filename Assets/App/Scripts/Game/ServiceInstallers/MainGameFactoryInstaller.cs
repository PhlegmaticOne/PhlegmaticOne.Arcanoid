using Common.Scenes;
using Game.Base;
using Game.Bonuses;
using Game.Field.Builder;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Game.Systems.Health;
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