using Common.Energy;
using Common.Game.Providers.Providers;
using Common.Scenes;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;

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
                var gameDataProvider = global.GetRequiredService<IGameDataProvider>();
                var energyManager = global.GetRequiredService<EnergyManager>();
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                var sceneChanger = global.GetRequiredService<ISceneChanger>();
                
                var pauseGameCommand = new PauseGameCommand(game);
                var continueGameCommand = new ContinueGameCommand(game);
                var changeCommand = new ChangeOnCloseControlCommand(popupManager, continueGameCommand);
                var restartCommand = new RestartControlCommand(energyManager, gameDataProvider, game, popupManager);
                var backCommand = new BackControlCommand(game, popupManager, sceneChanger);
                
                return new MainGameMenuViewModel
                {
                    ShowAction = new PopupAction(pauseGameCommand, NoneCommand.New),
                    CloseAction = PopupAction.Empty,
                    ContinueControlAction = new ControlAction(changeCommand),
                    RestartControlAction = new ControlAction(restartCommand),
                    BackControlAction = new ControlAction(backCommand),
                    CurrentPackGameData = gameDataProvider.GetGameData().PackGameData
                };
            });
        }
    }
}