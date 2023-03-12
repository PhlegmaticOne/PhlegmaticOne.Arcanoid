using Game.Behaviors;
using Game.PlayerObjects.BallObject;
using Libs.TimeActions;
using UnityEngine;

namespace Game.Bonuses.Behaviors.IncreaseBallsSpeed
{
    public class IncreaseBallsSpeedBehavior : IObjectBehavior<Bonus>
    {
        private readonly BallsOnField _ballsOnField;
        private readonly TimeActionsManager _timeActionsManager;
        private float _increaseSpeed;
        private float _executionTime;

        public IncreaseBallsSpeedBehavior(BallsOnField ballsOnField, TimeActionsManager timeActionsManager)
        {
            _ballsOnField = ballsOnField;
            _timeActionsManager = timeActionsManager;
        }

        public void SetBehaviorParameters(float increaseSpeed, float executionTime)
        {
            _increaseSpeed = increaseSpeed;
            _executionTime = executionTime;
        }

        public void Behave(Bonus entity, Collision2D collision2D)
        {
            _timeActionsManager.AddTimeAction(new IncreaseBallSpeedTimeAction(_executionTime, _ballsOnField, _increaseSpeed));
        }
    }
}