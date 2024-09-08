using Common.WinButton;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class WinButtonEnabledProviderInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWinButtonEnabledProvider>(new WinButtonEnabledProvider());
        }
    }
}