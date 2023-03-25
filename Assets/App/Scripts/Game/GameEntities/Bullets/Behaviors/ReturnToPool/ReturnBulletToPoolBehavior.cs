using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bullets.Behaviors.ReturnToPool
{
    public class ReturnBulletToPoolBehavior : IObjectBehavior<Bullet>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly BulletsOnField _bulletsOnField;

        public ReturnBulletToPoolBehavior(IPoolProvider poolProvider, BulletsOnField bulletsOnField)
        {
            _poolProvider = poolProvider;
            _bulletsOnField = bulletsOnField;
        }
        
        public bool IsDefault => true;
        
        public void Behave(Bullet entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Bullet>();
            pool.ReturnToPool(entity);
            _bulletsOnField.Remove(entity);
        }
    }
}