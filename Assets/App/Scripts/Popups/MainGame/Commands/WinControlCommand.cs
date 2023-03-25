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
        private readonly IPopupManager _popupManager;
        private readonly ControlSystem _controlSystem;
        private readonly EntitiesOnFieldCollection _entitiesOnFieldCollection;
        private readonly GameField _gameField;
        private readonly TimeActionsManager _timeActionsManager;

        private float _destroyInterval;

        public WinControlCommand(IPoolProvider poolProvider,
            IPopupManager popupManager,
            ControlSystem controlSystem,
            EntitiesOnFieldCollection entitiesOnFieldCollection,
            GameField gameField,
            TimeActionsManager timeActionsManager)
        {
            _poolProvider = poolProvider;
            _popupManager = popupManager;
            _controlSystem = controlSystem;
            _entitiesOnFieldCollection = entitiesOnFieldCollection;
            _gameField = gameField;
            _timeActionsManager = timeActionsManager;
        }

        public void SetCommandParameters(float destroyInterval)
        {
            _destroyInterval = destroyInterval;
        }
        
        protected override void Execute()
        {
            _popupManager.CurrentPopup.DisableInput();
            var tag = _entitiesOnFieldCollection.BallsOnField.MainBall.BehaviorObjectTags[0].Tag;
            _entitiesOnFieldCollection.ReturnToPool(_poolProvider);
            _controlSystem.DisableInput();

            var count = _gameField.GetDefaultBlocksCount();
            var action = new WinControlCommandTimeAction(_gameField, tag, _destroyInterval, count + 1);
            _timeActionsManager.AddTimeAction(action);
            _timeActionsManager.StopAllExcept(action);
        }
    }
}