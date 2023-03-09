using Game.Behaviors;
using Game.Field;
using UnityEngine;

namespace Game.Blocks.Behaviors.BallSpeed
{
    public class ChangeBallSpeedBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly int _startBlocksCount;
        private float _maxBallSpeed;

        public ChangeBallSpeedBehavior(GameField gameField)
        {
            _gameField = gameField;
            _startBlocksCount = gameField.Blocks.Count;
        }
        
        public void SetBehaviorParameters(float maxBallSpeed)
        {
            _maxBallSpeed = maxBallSpeed;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var notDestroyedBlocksCount = _gameField.NotDestroyedBlocksCount;
        }
    }
}