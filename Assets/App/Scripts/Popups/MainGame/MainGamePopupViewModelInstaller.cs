using Game;
using Game.Base;
using Game.Composites;
using Game.Field;
using Game.Logic.Systems.Control;
using Libs.Pooling.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Services;
using Libs.TimeActions;
using Popups.MainGame.Commands;
using UnityEngine;

namespace Popups.MainGame
{
    public class MainGamePopupViewModelInstaller : ServiceInstaller
    {
        [SerializeField] private float _blockDestroyTime = 0.1f;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var global = ServiceProviderAccessor.Global;
                var pool = global.GetRequiredService<IPoolProvider>();
                var control = x.GetRequiredService<ControlSystem>();
                var entitiesOnField = x.GetRequiredService<EntitiesOnFieldCollection>();
                var field = x.GetRequiredService<GameField>();
                var timeActionsManager = x.GetRequiredService<TimeActionsManager>();
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                var popupManager = global.GetRequiredService<IPopupManager>();
                
                var menuControlCommand = new MenuControlCommand(game, popupManager);
                var winControlCommand = new WinControlCommand(pool, game, popupManager, control, entitiesOnField, field, timeActionsManager);
                winControlCommand.SetCommandParameters(_blockDestroyTime);
                
                return new MainGamePopupViewModel
                {
                    ShowAction = PopupAction.Empty,
                    CloseAction = PopupAction.Empty,
                    MenuControlAction = new ControlAction(menuControlCommand),
                    WinControlAction = new ControlAction(winControlCommand)
                };
            });
        }
    }
}