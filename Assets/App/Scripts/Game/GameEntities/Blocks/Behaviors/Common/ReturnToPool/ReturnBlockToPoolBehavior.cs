using Game.Field;
using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.ReturnToPool
{
    public class ReturnBlockToPoolBehavior : IObjectBehavior<Block>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly GameField _gameField;

        public ReturnBlockToPoolBehavior(IPoolProvider poolProvider, GameField gameField)
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