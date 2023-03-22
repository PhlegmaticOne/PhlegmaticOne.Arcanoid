using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.RageBall
{
    public class RageBallBehavior : IObjectBehavior<Ball>
    {
        public void Behave(Ball entity, Collision2D collision2D)
        {
            if (collision2D.collider.gameObject.TryGetComponent<Block>(out var block))
            {
                var tag = entity.BehaviorObjectTags[0];
                block.DestroyWithTag(tag.Tag, collision2D);
            }
            
            var normal = collision2D.contacts[0].normal;
            var ballVelocity = entity.GetSpeed();
            var velocity = Vector3.Reflect(ballVelocity, normal);
            entity.SetSpeed(velocity);
        }
    }
}