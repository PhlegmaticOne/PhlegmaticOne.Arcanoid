using Common.WinButton;
using Game;
using Game.Base;
using Game.Common;
using Game.Composites;
using Game.Field;
using Game.Logic.Systems.Control;
using Libs.Pooling.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Libs.TimeActions;

namespace Popups.MainGame.Commands
{
    public class WinControlCommand : EmptyCommandBase
    {
        private readonly IPoolProvider _poolProvider;
        private readonly IWinButtonEnabledProvider _winButtonEnabledProvider;
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IPopupManager _popupManager;
        private readonly ControlSystem _controlSystem;
        private readonly EntitiesOnFieldCollection _entitiesOnFieldCollection;
        private readonly GameField _gameField;
        private readonly TimeActionsManager _timeActionsManager;

        private DynamicBlockAffectingInfo _dynamicBlockAffectingInfo;

        public WinControlCommand(IPoolProvider poolProvider,
            IWinButtonEnabledProvider winButtonEnabledProvider,
            IGame<MainGameData, MainGameEvents> game,
            IPopupManager popupManager,
            ControlSystem controlSystem,
            EntitiesOnFieldCollection entitiesOnFieldCollection,
            GameField gameField,
            TimeActionsManager timeActionsManager)
        {
            _poolProvider = poolProvider;
            _winButtonEnabledProvider = winButtonEnabledProvider;
            _game = game;
            _popupManager = popupManager;
            _controlSystem = controlSystem;
            _entitiesOnFieldCollection = entitiesOnFieldCollection;
            _gameField = gameField;
            _timeActionsManager = timeActionsManager;
        }

        protected override bool CanExecute() => _winButtonEnabledProvider.IsEnabled;

        public void SetCommandParameters(DynamicBlockAffectingInfo dynamicBlockAffectingInfo)
        {
            _dynamicBlockAffectingInfo = dynamicBlockAffectingInfo;
        }
        
        protected override void Execute()
        {
            _game.AnimatedWin = false;
            _popupManager.CurrentPopup.DisableInput();
            var tag = _entitiesOnFieldCollection.BallsOnField.All[0].BehaviorObjectTags[0].Tag;
            _entitiesOnFieldCollection.ReturnToPool(_poolProvider);
            _controlSystem.DisableInput();

            var count = _gameField.GetDefaultBlocksCount();
            var interval = _dynamicBlockAffectingInfo.GetAffectingInterval(count);
            var action = new WinControlCommandTimeAction(_gameField, tag, interval, count + 1);
            _timeActionsManager.AddTimeAction(action);
            _timeActionsManager.StopAllExcept(action);
        }
    }
}