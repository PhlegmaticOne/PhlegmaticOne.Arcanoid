using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Services;
using Popups.Common;

namespace Popups.Energy
{
    public class EnergyPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var closeCommand = new CloseLastPopupCommand(popupManager);
                return new EnergyPopupViewModel
                {
                    CloseAction = PopupAction.Empty,
                    ShowAction = PopupAction.Empty,
                    OkControlAction = new ControlAction(closeCommand)
                };
            });
        }
    }
}