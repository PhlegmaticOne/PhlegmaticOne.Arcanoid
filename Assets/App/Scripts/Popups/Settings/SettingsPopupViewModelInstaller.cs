using Common.Localization;
using Common.WinButton;
using Libs.Localization.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Services;
using Popups.ClearData;
using Popups.Common;
using Popups.Settings.Commands;

namespace Popups.Settings
{
    public class SettingsPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var winButtonEnabledProvider = x.GetRequiredService<IWinButtonEnabledProvider>();
                var localizationManager = x.GetRequiredService<ILocalizationManager>();
                var localizationProvider = x.GetRequiredService<ILocalizationProvider>();
                
                var closeLastPopupCommand = new CloseLastPopupCommand(popupManager);
                var winButtonEnabledCommand = new WinButtonEnabledCommand(winButtonEnabledProvider);
                var clearDataCommand = new SpawnPopupCommand<ClearDataPopup>(popupManager);
                var changeLocaleCommand = new ChangeLocalizationCommand(localizationManager, localizationProvider);
                
                return new SettingsPopupViewModel
                {
                    CloseAction = PopupAction.Empty,
                    ShowAction = PopupAction.Empty,
                    CloseControlAction = new ControlAction(closeLastPopupCommand),
                    ClearDataControlAction = new ControlAction(clearDataCommand),
                    WinButtonEnabledControlAction = new ControlAction(winButtonEnabledCommand),
                    ChangeLocaleControlAction = new ControlAction(changeLocaleCommand)
                };
            });
        }
    }
}