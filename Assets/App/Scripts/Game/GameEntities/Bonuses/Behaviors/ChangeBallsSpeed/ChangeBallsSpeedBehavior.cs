using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeBallsSpeed
{
    public class ChangeBallsSpeedBehavior : IObjectBehavior<Bonus>
    {
        private readonly BallsOnField _ballsOnField;
        private readonly TimeActionsManager _timeActionsManager;
        private float _speedToChange;
        private float _executionTime;
        private bool _isAdding;

        public ChangeBallsSpeedBehavior(BallsOnField ballsOnField, TimeActionsManager timeActionsManager)
        {
            _ballsOnField = ballsOnField;
            _timeActionsManager = timeActionsManager;
        }

        public void SetBehaviorParameters(float speedToChange, float executionTime, bool isAdding)
        {
            _speedToChange = speedToChange;
            _executionTime = executionTime;
            _isAdding = isAdding;
        }

        public void Behave(Bonus entity, Collision2D collision2D)
        {
            if (_timeActionsManager.TryGetAction<ChangeBallSpeedTimeAction>(out var action) &&
                action.IsAdding == _isAdding)
            {
                action.Restart();
            }
            else
            {
                _timeActionsManager.AddTimeAction(
                    new ChangeBallSpeedTimeAction(_executionTime, _ballsOnField, _speedToChange, _isAdding)); 
            }
        }
    }
}