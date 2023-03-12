using Game.Behaviors;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.BallDamage
{
    public class BallDamageBehavior : IObjectBehavior<Block>
    {
        public void Behave(Block entity, Collision2D collision2D)
        {
            entity.Damage();
        }
    }
}