using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.RageBall
{
    public class RageBallBonusBehavior : IObjectBehavior<Bonus>
    {
        private readonly TimeActionsManager _timeActionsManager;
        private readonly BallsOnField _ballsOnField;
        private readonly ColliderTag _blockColliderTag;

        private float _actionTime;

        public RageBallBonusBehavior(TimeActionsManager timeActionsManager,
            BallsOnField ballsOnField,
            ColliderTag blockColliderTag)
        {
            _timeActionsManager = timeActionsManager;
            _ballsOnField = ballsOnField;
            _blockColliderTag = blockColliderTag;
        }

        public void SetBehaviorParameters(float actionTime) => _actionTime = actionTime;

        public void Behave(Bonus entity, Collision2D collision2D)
        {
            _timeActionsManager.AddTimeAction(new RageBallTimeAction(_ballsOnField, _blockColliderTag, _actionTime));
        }
    }
}