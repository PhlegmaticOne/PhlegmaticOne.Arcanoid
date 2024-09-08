using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.ChangePlatformScale
{
    public class ChangePlatformScaleBehavior : IObjectBehavior<Bonus>
    {
        private readonly TimeActionsManager _timeActionsManager;
        private readonly Ship _ship;

        private float _actionTime;
        private float _changingTime;
        private float _scaleBy;
        private bool _isIncrease;

        public ChangePlatformScaleBehavior(TimeActionsManager timeActionsManager, Ship ship)
        {
            _timeActionsManager = timeActionsManager;
            _ship = ship;
        }
        
        public bool IsDefault => false;

        public void SetBehaviorParameters(float actionTime, float changingTime, float scaleBy, bool isIncrease)
        {
            _actionTime = actionTime;
            _changingTime = changingTime;
            _scaleBy = scaleBy;
            _isIncrease = isIncrease;
        }
        
        public void Behave(Bonus entity, Collision2D collision2D)
        {
            if (_timeActionsManager
                .TryGetAction<ChangePlatformScaleTimeAction>(x => x.IsIncrease == _isIncrease, out var action))
            {
                action.Restart();
            }
            else
            {
                _timeActionsManager.AddTimeAction(new ChangePlatformScaleTimeAction(
                    _ship, _scaleBy, _changingTime, _isIncrease, _actionTime));
            }
        }
    }
}