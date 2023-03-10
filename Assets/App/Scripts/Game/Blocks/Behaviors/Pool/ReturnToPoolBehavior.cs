using Game.Accessors;
using Game.Behaviors;
using Game.Field;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Behaviors.Pool
{
    public class ReturnToPoolBehavior : IObjectBehavior<Block>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly IObjectAccessor<GameField> _gameFieldAccessor;

        public ReturnToPoolBehavior(IPoolProvider poolProvider, IObjectAccessor<GameField> gameFieldAccessor)
        {
            _poolProvider = poolProvider;
            _gameFieldAccessor = gameFieldAccessor;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Block>();
            var field = _gameFieldAccessor.Get();
            pool.ReturnToPool(entity);
            field.RemoveBlock(entity);
        }
    }
}