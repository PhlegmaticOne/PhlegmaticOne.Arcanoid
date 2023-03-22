using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.PackChoose;
using Popups.Settings;
using Popups.Start.Commands;

namespace Popups.Start
{
    public class StartPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                
                var spawnSettingsPopupCommand = new SpawnPopupCommand<SettingsPopup>(popupManager);
                var spawnPacksPopupCommand = new SpawnPopupCommand<PackChoosePopup>(popupManager);
                var exitCommand = new ExitGameCommand();
                var closeCommand = new ChangeOnCloseControlCommand(popupManager, spawnPacksPopupCommand);
                
                var viewModel = new StartPopupViewModel
                {
                    CloseAction = new PopupAction(NoneCommand.New, NoneCommand.New),
                    ShowAction = new PopupAction(NoneCommand.New, NoneCommand.New),
                    SettingsControlAction = new ControlAction(spawnSettingsPopupCommand),
                    ExitControlAction = new ControlAction(exitCommand),
                    PlayControlAction = new ControlAction(closeCommand)
                };

                return viewModel;
            });
        }
    }
}