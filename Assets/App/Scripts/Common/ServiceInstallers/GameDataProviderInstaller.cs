using Common.Data.Providers;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class GameDataProviderInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new GameDataProvider());
        }
    }
}