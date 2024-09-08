using Common.Localization;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class LocalizationProviderInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ILocalizationProvider>(new LocalizationProvider());
        }
    }
}