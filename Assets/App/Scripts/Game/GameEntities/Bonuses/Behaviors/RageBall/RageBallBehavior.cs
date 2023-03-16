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
            var ballTag = entity.GetComponent<BehaviorObjectTags>().ColliderTags[0];
            
            if (collision2D.collider.gameObject.TryGetComponent<Block>(out var block))
            {
                block.DestroyWithTag(ballTag.Tag, collision2D);
            }
            
            var normal = collision2D.contacts[0].normal;
            var ballVelocity = entity.GetSpeed();
            var velocity = Vector3.Reflect(ballVelocity, normal);
            entity.SetSpeed(velocity);
        }
    }
}