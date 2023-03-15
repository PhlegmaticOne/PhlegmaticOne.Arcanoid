using System.Collections.Generic;
using System.Linq;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
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
                new RemoveFromBallsOnFieldBehavior(_ballsOnField),
                new ReturnBallToPoolBehavior(_poolProvider)
            };
            
            Subscribe();
        }

        private void BallsOnFieldOnBallAdded(Ball ball) => 
            AddNewBehaviorsToBall(ball, _behaviorsToReplace);

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
            _startBallOnDestroyBehaviors = GetBottomOnDestroyBehaviors();

            foreach (var ball in _ballsOnField.All)
            {
                AddNewBehaviorsToBall(ball, _behaviorsToReplace);
            }
        }

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd()
        {
            if (_shouldStopOnTimeEnd == false)
            {
                Debug.Log("Stop");
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
            
            RestoreRemainedBallBehaviors();
        }

        private void RestoreRemainedBallBehaviors() => 
            AddNewBehaviorsToBall(_ballsOnField.All[0], _startBallOnDestroyBehaviors);

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