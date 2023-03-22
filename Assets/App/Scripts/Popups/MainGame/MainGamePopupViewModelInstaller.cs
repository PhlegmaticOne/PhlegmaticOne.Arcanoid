using Common.Bag;
using Common.Packs.Data.Repositories.Base;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.MainGame.Commands;
using Popups.Win;

namespace Popups.MainGame
{
    public class MainGamePopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var global = ServiceProviderAccessor.Global;
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                var objectBag = global.GetRequiredService<IObjectBag>();
                var levelRepository = global.GetRequiredService<ILevelRepository>();
                var popupManager = global.GetRequiredService<IPopupManager>();
                
                var startGameCommand = new MainGamePopupOnShowCommand(game, objectBag, levelRepository);
                var menuControlCommand = new MenuControlCommand(game, popupManager);
                var winControlCommand = new SpawnPopupCommand<WinPopup>(popupManager);
                
                return new MainGamePopupViewModel
                {
                    ShowAction = new PopupAction(NoneCommand.New, startGameCommand),
                    CloseAction = PopupAction.Empty,
                    MenuControlAction = new ControlAction(menuControlCommand),
                    WinControlAction = new ControlAction(winControlCommand)
                };
            });
        }
    }
}