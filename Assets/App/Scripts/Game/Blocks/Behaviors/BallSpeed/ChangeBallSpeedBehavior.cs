using Game.Accessors;
using Game.Behaviors;
using Game.Field;
using Game.PlayerObjects.BallObject;
using UnityEngine;

namespace Game.Blocks.Behaviors.BallSpeed
{
    public class ChangeBallSpeedBehavior : IObjectBehavior<Block>
    {
        private readonly IObjectAccessor<GameField> _gameFieldAccessor;
        private readonly IObjectAccessor<BallsOnField> _ballsOnFieldAccessor;
        private float _increaseSpeed;

        public ChangeBallSpeedBehavior(
            IObjectAccessor<GameField> gameFieldAccessor,
            IObjectAccessor<BallsOnField> ballsOnFieldAccessor)
        {
            _gameFieldAccessor = gameFieldAccessor;
            _ballsOnFieldAccessor = ballsOnFieldAccessor;
        }
        
        public void SetBehaviorParameters(float increaseSpeed)
        {
            _increaseSpeed = increaseSpeed;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var field = _gameFieldAccessor.Get();
            var blocksCount = field.Width * field.Height;
            var notDestroyedBlocksCount = field.ActiveBlocksCount;

            foreach (var ball in _ballsOnFieldAccessor.Get().All)
            {
                var ballStartSpeed = ball.GetStartSpeed();
                var ballSpeedNormalized = ball.GetSpeed().normalized;
                var deltaSpeed = _increaseSpeed * (blocksCount - notDestroyedBlocksCount) / blocksCount;
                var newSpeed = deltaSpeed + ballStartSpeed;
                ball.SetSpeed(newSpeed * ballSpeedNormalized);
            }
        }
    }
}