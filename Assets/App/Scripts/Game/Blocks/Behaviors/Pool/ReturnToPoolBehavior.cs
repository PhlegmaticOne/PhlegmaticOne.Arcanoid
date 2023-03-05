using Game.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Behaviors.Pool
{
    public class ReturnToPoolBehavior : IObjectBehavior<Block>
    {
        private readonly IPoolProvider _poolProvider;

        public ReturnToPoolBehavior(IPoolProvider poolProvider)
        {
            _poolProvider = poolProvider;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Block>();
            pool.ReturnToPool(entity);
        }
    }
}