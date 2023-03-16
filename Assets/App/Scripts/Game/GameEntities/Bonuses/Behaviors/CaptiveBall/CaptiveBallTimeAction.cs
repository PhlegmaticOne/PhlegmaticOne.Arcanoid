using System.Collections.Generic;
using System.Linq;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom;
using Libs.Behaviors;
using Libs.Pooling.Base;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallTimeAction : TimeActionBase
    {
        private readonly IPoolProvider _poolProvider;
        private readonly BallsOnField _ballsOnField;
        private readonly ColliderTag _bottomColliderTag;

        private List<IObjectBehavior<Ball>> _startBallOnDestroyBehaviors;

        private bool _shouldStopOnTimeEnd = true;

        private readonly List<IObjectBehavior<Ball>> _behaviorsToReplace;

        public CaptiveBallTimeAction(float executionTime, IPoolProvider poolProvider,
            BallsOnField ballsOnField, ColliderTag bottomColliderTag) :
            base(executionTime)
        {
            _poolProvider = poolProvider;
            _ballsOnField = ballsOnField;
            _bottomColliderTag = bottomColliderTag;
            
            _behaviorsToReplace = new List<IObjectBehavior<Ball>>
            {
                new ReturnBallToPoolBehavior(_poolProvider),
                new RemoveFromBallsOnFieldBehavior(_ballsOnField)
            };
        }

        private void BallsOnFieldOnBallAdded(Ball ball)
        {
            var firstBall = _ballsOnField.All[0];
            firstBall.InstallOnCollisionBehaviorsTo(ball);
            AddNewBehaviorsToBall(ball, _behaviorsToReplace);
        }

        private void BallsOnFieldOnBallRemoved(Ball ball)
        {
            if (_ballsOnField.All.Count == 1)
            {
                Unsubscribe();
                RestoreRemainedBallBehaviors();
                _shouldStopOnTimeEnd = false;
            }
        }

        public override void OnStart()
        {
            Subscribe();
            
            _startBallOnDestroyBehaviors = GetBottomOnDestroyBehaviors();

            var firstBall = _ballsOnField.All[0];
            AddNewBehaviorsToBall(firstBall, _behaviorsToReplace);
            
            for (var i = 1; i < _ballsOnField.All.Count; i++)
            {
                var newBall = _ballsOnField.All[i];
                firstBall.InstallOnCollisionBehaviorsTo(newBall);
                AddNewBehaviorsToBall(newBall, _behaviorsToReplace);
            }
        }

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd()
        {
            if (_shouldStopOnTimeEnd == false)
            {
                return;
            }

            Unsubscribe();
            
            var pool = _poolProvider.GetPool<Ball>();
            var count = _ballsOnField.All.Count;
            
            for (var i = count - 1; i >= 1; i--)
            {
                var ball = _ballsOnField.All[i];
                pool.ReturnToPool(ball);
                _ballsOnField.RemoveBall(ball);
            }

            if (_ballsOnField.All.Count != 0)
            {
                RestoreRemainedBallBehaviors();
            }
        }

        private void RestoreRemainedBallBehaviors()
        {
            AddNewBehaviorsToBall(_ballsOnField.All[0], _startBallOnDestroyBehaviors);
        }

        private void AddNewBehaviorsToBall(Ball ball, List<IObjectBehavior<Ball>> behaviors)
        {
            var tag = _bottomColliderTag.Tag;
            var onDestroyBehaviors = ball.OnDestroyBehaviors;
            
            onDestroyBehaviors.ClearBehaviors(tag);

            foreach (var behavior in behaviors)
            {
                onDestroyBehaviors.AddBehavior(tag, behavior);
            }
        }

        private void Subscribe()
        {
            _ballsOnField.BallRemoved += BallsOnFieldOnBallRemoved;
            _ballsOnField.BallAdded += BallsOnFieldOnBallAdded;
        }

        private void Unsubscribe()
        {
            _ballsOnField.BallRemoved -= BallsOnFieldOnBallRemoved;
            _ballsOnField.BallAdded -= BallsOnFieldOnBallAdded;
        }

        private List<IObjectBehavior<Ball>> GetBottomOnDestroyBehaviors() =>
            _ballsOnField.All[0].OnDestroyBehaviors.GetAllBehaviors(_bottomColliderTag.Tag).ToList();
    }
}