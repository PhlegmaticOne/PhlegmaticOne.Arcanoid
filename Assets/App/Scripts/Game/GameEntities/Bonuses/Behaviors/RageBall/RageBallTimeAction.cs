using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Behaviors.Movement;
using Libs.Behaviors;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.RageBall
{
    public class RageBallTimeAction : EndsOfIntervalTimeAction
    {
        private readonly BallsOnField _ballsOnField;
        private readonly ColliderTag _blockColliderTag;

        private MovementAngleCorrectionBehavior _tempAction;

        public RageBallTimeAction(BallsOnField ballsOnField, ColliderTag blockColliderTag, float executionTime) : 
            base(executionTime)
        {
            _ballsOnField = ballsOnField;
            _blockColliderTag = blockColliderTag;
        }

        public override void OnStart()
        {
            var colliderTag = _blockColliderTag.Tag;

            _tempAction = _ballsOnField.All[0]
                .OnCollisionBehaviors.GetBehavior<MovementAngleCorrectionBehavior>(colliderTag);
            
            foreach (var ball in _ballsOnField.All)
            {
                var onCollisionBehaviors = ball.OnCollisionBehaviors;
                onCollisionBehaviors
                    .SubstituteBehavior<MovementAngleCorrectionBehavior>(colliderTag, new RageBallBehavior());
                ball.ChangeRageMode(true);
            }
        }

        public override void OnEnd()
        {
            var colliderTag = _blockColliderTag.Tag;
            
            foreach (var ball in _ballsOnField.All)
            {
                var onCollisionBehaviors = ball.OnCollisionBehaviors;
                onCollisionBehaviors
                    .SubstituteBehavior<RageBallBehavior>(colliderTag, _tempAction);
                ball.ChangeRageMode(false);
            }

            _tempAction = null;
        }
    }
}