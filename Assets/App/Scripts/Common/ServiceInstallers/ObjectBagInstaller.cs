using Common.Bag;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class ObjectBagInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IObjectBag>(new ObjectBag());
        }
    }
}