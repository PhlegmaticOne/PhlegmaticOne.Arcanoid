using Common.Game.Providers.Providers;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class GameDataProviderInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton<IObjectBag>(new ObjectBag());
            serviceCollection.AddSingleton<IGameDataProvider>(new GameDataProvider());
        }
    }
}