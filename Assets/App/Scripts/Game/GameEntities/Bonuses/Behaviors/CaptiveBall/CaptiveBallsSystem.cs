using System.Collections.Generic;
using System.Linq;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom;
using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallsSystem : MonoBehaviour
    {
        [SerializeField] private ColliderTag _bottomColliderTag;
        
        private BallsOnField _ballsOnField;

        private List<IObjectBehavior<Ball>> _behaviorsToReplace;
        private List<IObjectBehavior<Ball>> _startBallOnDestroyBehaviors;

        private bool _isInitialState = true;

        public void Initialize(IPoolProvider poolProvider, BallsOnField ballsOnField)
        {
            _isInitialState = true;
            _ballsOnField = ballsOnField;
            _behaviorsToReplace = new List<IObjectBehavior<Ball>>
            {
                new ReturnBallToPoolBehavior(poolProvider),
                new RemoveFromBallsOnFieldBehavior(_ballsOnField)
            };

            _startBallOnDestroyBehaviors = GetBottomOnDestroyBehaviors();
            Subscribe();
        }

        public void AddNewBalls(IEnumerable<Ball> balls)
        {
            var mainBall = _ballsOnField.All[0];
            
            if (_isInitialState)
            {
                AddNewBehaviorsToBall(mainBall, _behaviorsToReplace);
                _isInitialState = false;
            }

            foreach (var ball in balls)
            {
                AddNewBehaviorsToBall(ball, _behaviorsToReplace);
            }
        }
        
        public void Disable()
        {
            Unsubscribe();
        }

        private void BallsOnFieldOnBallRemoved(Ball ball)
        {
            if (_ballsOnField.All.Count == 1)
            {
                _isInitialState = true;
                AddNewBehaviorsToBall(_ballsOnField.All[0], _startBallOnDestroyBehaviors);
            }
        }
        
        private void AddNewBehaviorsToBall(Ball ball, List<IObjectBehavior<Ball>> behaviors)
        {
            if (ball == null)
            {
                return;
            }
            
            var colliderTag = _bottomColliderTag.Tag;
            var onDestroyBehaviors = ball.OnDestroyBehaviors;
            
            onDestroyBehaviors.ClearBehaviors(colliderTag);

            foreach (var behavior in behaviors)
            {
                onDestroyBehaviors.AddBehavior(colliderTag, behavior);
            }
        }

        private void Subscribe() => _ballsOnField.BallRemoved += BallsOnFieldOnBallRemoved;

        private void Unsubscribe() => _ballsOnField.BallRemoved -= BallsOnFieldOnBallRemoved;

        private List<IObjectBehavior<Ball>> GetBottomOnDestroyBehaviors() =>
            _ballsOnField.All[0].OnDestroyBehaviors.GetAllBehaviors(_bottomColliderTag.Tag).ToList();
    }
}