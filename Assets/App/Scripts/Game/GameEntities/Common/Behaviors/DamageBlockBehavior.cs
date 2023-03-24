using Game.GameEntities.Base;
using Game.GameEntities.Blocks;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Common
{
    public class DamageBlockBehavior
    {
        public void Damage(Block block, float damage, string tag, Collision2D collision2D)
        {
            if (block.IsDestroyed || block.IsActive == false)
            {
                return;
            }

            block.Damage(damage);
        
            if (block.CurrentHealth <= 0)
            {
                block.DestroyWithTag(tag, collision2D);
            }
        }
    }
    
    public class DamageBlockBehavior<T> : IObjectBehavior<T>
        where T : BehaviorObject<T>, IDamageable
    {
        private readonly DamageBlockBehavior _damageBlockBehavior;
        public DamageBlockBehavior()
        {
            _damageBlockBehavior = new DamageBlockBehavior();
        }
        
        public void Behave(T entity, Collision2D collision2D)
        {
            if (collision2D.collider.TryGetComponent<Block>(out var block) == false)
            {
                return;
            }
            
            _damageBlockBehavior.Damage(block, entity.Damage, entity.BehaviorObjectTags[0].Tag, collision2D);
        }
    }
}