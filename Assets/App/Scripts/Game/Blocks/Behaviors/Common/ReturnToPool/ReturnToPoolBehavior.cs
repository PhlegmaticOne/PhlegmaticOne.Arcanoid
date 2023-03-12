using Game.Behaviors;
using Game.Field;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.ReturnToPool
{
    public class ReturnToPoolBehavior : IObjectBehavior<Block>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly GameField _gameField;

        public ReturnToPoolBehavior(IPoolProvider poolProvider, GameField gameField)
        {
            _poolProvider = poolProvider;
            _gameField = gameField;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Block>();
            pool.ReturnToPool(entity);
            _gameField.RemoveBlock(entity);
        }
    }
}