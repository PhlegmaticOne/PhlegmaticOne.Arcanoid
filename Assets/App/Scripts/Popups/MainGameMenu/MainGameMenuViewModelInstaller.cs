using Common.Bag;
using Common.Energy;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.Common.Commands;
using UnityEngine;

namespace Popups.MainGameMenu
{
    public class MainGameMenuViewModelInstaller : ServiceInstaller
    {
        [SerializeField] private string _restartReasonPhraseKey;
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
                var restartCommandCantExecuteHandler =
                    new ShowEnergyPopupCommand(popupManager, _restartReasonPhraseKey);
                
                var backCommand = new BackControlCommand(game, popupManager);
                
                return new MainGameMenuViewModel
                {
                    ShowAction = new PopupAction(pauseGameCommand, NoneCommand.New),
                    CloseAction = PopupAction.Empty,
                    ContinueControlAction = new ControlAction(changeCommand),
                    RestartControlAction = new ControlAction(restartCommand, restartCommandCantExecuteHandler),
                    BackControlAction = new ControlAction(backCommand)
                };
            });
        }
    }
}