using Game.GameEntities.Bonuses.Behaviors.ChangePlatformScale;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.PlatformSpeed
{
    public class ChangePlatformSpeedBehavior : IObjectBehavior<Bonus>
    {
        private readonly TimeActionsManager _timeActionsManager;
        private readonly Ship _ship;

        private float _actionTime;
        private float _changeBy;
        private bool _isIncrease;

        public ChangePlatformSpeedBehavior(TimeActionsManager timeActionsManager, Ship ship)
        {
            _timeActionsManager = timeActionsManager;
            _ship = ship;
        }
        
        public bool IsDefault => false;

        public void SetBehaviorParameters(float actionTime, float changeBy, bool isIncrease)
        {
            _actionTime = actionTime;
            _changeBy = changeBy;
            _isIncrease = isIncrease;
        }
        
        public void Behave(Bonus entity, Collision2D collision2D)
        {
            if (_timeActionsManager
                .TryGetAction<ChangePlatformSpeedTimeAction>(x => x.IsIncrease == _isIncrease, out var action))
            {
                action.Restart();
            }
            else
            {
                _timeActionsManager.AddTimeAction(new ChangePlatformSpeedTimeAction(_ship,
                    _changeBy, _isIncrease, _actionTime));
            }
        }
    }
}