using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.Common.Commands;

namespace Popups.MainGameMenu
{
    public class MainGameMenuViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var global = ServiceProviderAccessor.Global;
                var popupManager = global.GetRequiredService<IPopupManager>();
                var objectBag = global.GetRequiredService<IObjectBag>();
                var energyManager = global.GetRequiredService<EnergyManager>();
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                
                
                var pauseGameCommand = new PauseGameCommand(game);
                var continueGameCommand = new ContinueGameCommand(game);
                var changeCommand = new ChangeOnCloseControlCommand(popupManager, continueGameCommand);
                var restartCommand = new RestartControlCommand(energyManager, objectBag, game, popupManager);
                var backCommand = new BackControlCommand(game, popupManager);
                
                return new MainGameMenuViewModel
                {
                    ShowAction = new PopupAction(pauseGameCommand, NoneCommand.New),
                    CloseAction = PopupAction.Empty,
                    ContinueControlAction = new ControlAction(changeCommand),
                    RestartControlAction = new ControlAction(restartCommand),
                    BackControlAction = new ControlAction(backCommand),
                    CurrentPackGameData = objectBag.Get<GameData>().PackGameData
                };
            });
        }
    }
}