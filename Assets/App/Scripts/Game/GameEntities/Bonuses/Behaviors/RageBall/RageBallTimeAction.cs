using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Behaviors.Blocks;
using Libs.Behaviors;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.RageBall
{
    public class RageBallTimeAction : TimeActionBase
    {
        private readonly BallsOnField _ballsOnField;
        private readonly ColliderTag _blockColliderTag;

        public RageBallTimeAction(BallsOnField ballsOnField, ColliderTag blockColliderTag, float executionTime) : 
            base(executionTime)
        {
            _ballsOnField = ballsOnField;
            _blockColliderTag = blockColliderTag;
        }

        public override void OnStart()
        {
            var colliderTag = _blockColliderTag.Tag;
            
            foreach (var ball in _ballsOnField.All)
            {
                var onCollisionBehaviors = ball.OnCollisionBehaviors;
                onCollisionBehaviors
                    .SubstituteBehavior<BlockAngleCorrectionBehavior>(colliderTag, new RageBallBehavior());
            }
        }

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd()
        {
            var colliderTag = _blockColliderTag.Tag;
            
            foreach (var ball in _ballsOnField.All)
            {
                var onCollisionBehaviors = ball.OnCollisionBehaviors;
                onCollisionBehaviors
                    .SubstituteBehavior<RageBallBehavior>(colliderTag, new BlockAngleCorrectionBehavior());
            }
        }
    }
}