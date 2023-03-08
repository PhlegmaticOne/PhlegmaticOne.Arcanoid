using Game;
using Game.Base;
using Game.Field.Builder;
using Game.PlayerObjects.BallObject.Spawners;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class MainGameFactoryInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGameFactory<MainGameRequires, MainGame>>(x =>
            {
                var ballSpawner = x.GetRequiredService<IBallSpawner>();
                var fieldBuilder = x.GetRequiredService<IFieldBuilder>();
                var inputSystem = x.GetRequiredService<IInputSystem>();
                var poolProvider = x.GetRequiredService<IPoolProvider>();
                return new MainGameFactory(fieldBuilder, poolProvider, ballSpawner, inputSystem);
            });
        }
    }
}