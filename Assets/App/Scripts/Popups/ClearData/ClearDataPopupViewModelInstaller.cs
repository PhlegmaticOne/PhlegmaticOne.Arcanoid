using Common.Energy.Repositories;
using Common.Packs.Data.Repositories.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.Settings.Commands;

namespace Popups.ClearData
{
    public class ClearDataPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var closeCommand = new CloseLastPopupCommand(popupManager);
                var packRepository = x.GetRequiredService<IPackRepository>();
                var energyRepository = x.GetRequiredService<IEnergyRepository>();
                var clearDataCommand = new ClearPlayerDataCommand(packRepository, energyRepository, popupManager);

                return new ClearDataPopupViewModel
                {
                    ShowAction = PopupAction.Empty,
                    CloseAction = PopupAction.Empty,
                    AcceptControlAction = new ControlAction(clearDataCommand),
                    CancelControlAction = new ControlAction(closeCommand)
                };
            });
        }
    }
}