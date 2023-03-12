using Game.Behaviors;
using Game.Field;
using Game.PlayerObjects.BallObject;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.IncreaseBallSpeed
{
    public class IncreaseBallSpeedBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly BallsOnField _ballsOnField;
        private float _increaseSpeed;

        public IncreaseBallSpeedBehavior(GameField gameField, BallsOnField ballsOnField)
        {
            _gameField = gameField;
            _ballsOnField = ballsOnField;
        }
        
        public void SetBehaviorParameters(float increaseSpeed)
        {
            _increaseSpeed = increaseSpeed;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var blocksCount = _gameField.Width * _gameField.Height;
            var notDestroyedBlocksCount = _gameField.ActiveBlocksCount;

            foreach (var ball in _ballsOnField.All)
            {
                var ballStartSpeed = ball.GetInitialSpeed();
                var ballSpeedNormalized = ball.GetSpeed().normalized;
                var deltaSpeed = _increaseSpeed * (blocksCount - notDestroyedBlocksCount) / blocksCount;
                var newSpeed = deltaSpeed + ballStartSpeed;
                ball.SetSpeed(newSpeed * ballSpeedNormalized);
            }
        }
    }
}