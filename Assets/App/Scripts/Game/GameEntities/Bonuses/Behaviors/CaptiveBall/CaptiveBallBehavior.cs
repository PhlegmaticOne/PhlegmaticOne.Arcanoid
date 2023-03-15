using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors;
using Libs.Pooling.Base;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallBehavior : IObjectBehavior<Block>
    {
        private readonly TimeActionsManager _timeActionsManager;
        private readonly IBallSpawner _ballSpawner;
        private readonly IPoolProvider _poolProvider;
        private readonly BallsOnField _ballsOnField;
        private readonly ColliderTag _bottomColliderTag;

        private float _actionTime;

        public CaptiveBallBehavior(TimeActionsManager timeActionsManager,
            IBallSpawner ballSpawner,
            IPoolProvider poolProvider, 
            BallsOnField ballsOnField,
            ColliderTag bottomColliderTag)
        {
            _timeActionsManager = timeActionsManager;
            _ballSpawner = ballSpawner;
            _poolProvider = poolProvider;
            _ballsOnField = ballsOnField;
            _bottomColliderTag = bottomColliderTag;
        }

        public void SetBehaviorParameters(float actionTime) => _actionTime = actionTime;

        public void Behave(Block entity, Collision2D collision2D)
        {
            if (collision2D.collider.gameObject.TryGetComponent<Ball>(out var ball))
            {
                AddNewBalls(ball);

                if (_timeActionsManager.ContainsAction<CaptiveBallTimeAction>())
                {
                    return;
                }
                
                _timeActionsManager.AddTimeAction(new CaptiveBallTimeAction(_actionTime,
                    _poolProvider, _ballsOnField, _bottomColliderTag));
            }
        }

        private void AddNewBalls(Ball original)
        {
            var count = _ballsOnField.All.Count;
            var speed = original.GetSpeed();
            
            for (var i = 0; i < count; i++)
            {
                var newBall = _ballSpawner.CreateBall(new BallCreationContext
                {
                    Position = original.transform.position - (i + 1) * original.GetCollider().bounds.size,
                    StartSpeed = speed.magnitude,
                    SetSpecifiedStartSpeed = true
                });
            
                newBall.SetDirection(speed.normalized);
                newBall.StartMove();
                _ballsOnField.AddBall(newBall);
            }
        }
    }
}