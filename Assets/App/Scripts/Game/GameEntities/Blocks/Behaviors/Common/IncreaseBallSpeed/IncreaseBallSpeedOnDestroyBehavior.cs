using Game.Field;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.IncreaseBallSpeed
{
    public class IncreaseBallSpeedOnDestroyBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly BallsOnField _ballsOnField;
        private float _increaseSpeed;

        public IncreaseBallSpeedOnDestroyBehavior(GameField gameField, BallsOnField ballsOnField)
        {
            _gameField = gameField;
            _ballsOnField = ballsOnField;
        }
        
        public void SetBehaviorParameters(float increaseSpeed) => _increaseSpeed = increaseSpeed;

        public void Behave(Block entity, Collision2D collision2D)
        {
            var deltaSpeed = _increaseSpeed / _gameField.StartDefaultBlocksCount;
            foreach (var ball in _ballsOnField.All)
            {
                ball.AddDeltaSpeed(deltaSpeed);
            }
        }
    }
}