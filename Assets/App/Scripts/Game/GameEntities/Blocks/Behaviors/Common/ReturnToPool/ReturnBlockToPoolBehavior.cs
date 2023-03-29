using DG.Tweening;
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
        private float _disappearTime;

        public ReturnBlockToPoolBehavior(IPoolProvider poolProvider, GameField gameField)
        {
            _poolProvider = poolProvider;
            _gameField = gameField;
        }

        public void SetBehaviorParameters(float disappearTime)
        {
            _disappearTime = disappearTime;
        }
        
        public bool IsDefault => true;
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Block>();
            entity.Disable();
            _gameField.RemoveBlock(entity);
            
            entity.transform.DOScale(Vector3.zero, _disappearTime)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => pool.ReturnToPool(entity))
                .SetUpdate(true).Play();
        }
    }
}