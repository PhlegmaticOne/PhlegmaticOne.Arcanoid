using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.Damage
{
    public class GetDamageBehavior : IObjectBehavior<Block>
    {
        public void Behave(Block entity, Collision2D collision2D) => entity.Damage();
    }
}