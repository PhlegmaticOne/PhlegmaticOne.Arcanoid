using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.BallDamage
{
    public class BallDamageBehavior : IObjectBehavior<Block>
    {
        public void Behave(Block entity, Collision2D collision2D)
        {
            entity.Damage();
        }
    }
}