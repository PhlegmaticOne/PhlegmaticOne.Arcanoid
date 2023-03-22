using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Services;
using Popups.Common;

namespace Popups.Settings
{
    public class SettingsPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var closeLastPopupCommand = new CloseLastPopupCommand(popupManager);

                return new SettingsPopupViewModel
                {
                    CloseAction = PopupAction.Empty,
                    ShowAction = PopupAction.Empty,
                    CloseControlAction = new ControlAction(closeLastPopupCommand)
                };
            });
        }
    }
}