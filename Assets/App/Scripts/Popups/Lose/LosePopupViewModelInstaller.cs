using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Scenes;
using Game;
using Game.Base;
using Game.Logic.Systems.Health;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;
using Libs.Services;
using Popups.Common;
using Popups.Common.Commands;
using Popups.Lose.Commands;
using UnityEngine;

namespace Popups.Lose
{
    public class LosePopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(x =>
            {
                var global = ServiceProviderAccessor.Global;
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                var popupManager = global.GetRequiredService<IPopupManager>();
                var energyManager = global.GetRequiredService<EnergyManager>();
                var objectBag = global.GetRequiredService<IObjectBag>();
                var healthSystem = x.GetRequiredService<HealthSystem>();
                var sceneChanger = global.GetRequiredService<ISceneChanger>();
                
                var pauseGameCommand = new PauseGameCommand(game);
                var backControlCommand = new BackControlCommand(game, popupManager, sceneChanger);
                
                var restartControlCommand = new RestartControlCommand(energyManager, objectBag, game, popupManager);
                var buyLifeControlCommand = new BuyLifeControlCommand(energyManager, healthSystem, objectBag, game, popupManager);

                return new LosePopupViewModel
                {
                    CloseAction = PopupAction.Empty,
                    ShowAction = new PopupAction(pauseGameCommand, NoneCommand.New),
                    BackControlAction = new ControlAction(backControlCommand),
                    BuyLifeControlAction = new ControlAction(buyLifeControlCommand),
                    RestartControlAction = new ControlAction(restartControlCommand),
                    CurrentPack = objectBag.Get<GameData>().PackGameData
                };
            });
        }
    }
}