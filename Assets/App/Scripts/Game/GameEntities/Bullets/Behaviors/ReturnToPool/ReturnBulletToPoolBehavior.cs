using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bullets.Behaviors.ReturnToPool
{
    public class ReturnBulletToPoolBehavior : IObjectBehavior<Bullet>
    {
        private readonly IPoolProvider _poolProvider;

        public ReturnBulletToPoolBehavior(IPoolProvider poolProvider)
        {
            _poolProvider = poolProvider;
        }
        
        public void Behave(Bullet entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Bullet>();
            pool.ReturnToPool(entity);
        }
    }
}