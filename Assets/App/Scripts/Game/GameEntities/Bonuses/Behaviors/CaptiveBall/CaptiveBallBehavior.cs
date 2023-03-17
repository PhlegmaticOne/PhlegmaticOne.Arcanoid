using System.Collections.Generic;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallBehavior : IObjectBehavior<Block>
    {
        private readonly IBallSpawner _ballSpawner;
        private readonly BallsOnField _ballsOnField;
        private readonly CaptiveBallsSystem _captiveBallsSystem;

        public CaptiveBallBehavior(IBallSpawner ballSpawner, BallsOnField ballsOnField, 
            CaptiveBallsSystem captiveBallsSystem)
        {
            _ballSpawner = ballSpawner;
            _ballsOnField = ballsOnField;
            _captiveBallsSystem = captiveBallsSystem;
        }

        public void Behave(Block entity, Collision2D collision2D)
        {
            if (TryGetBallFromCollision(collision2D, out var ball))
            {
                _captiveBallsSystem.AddNewBalls(CreateBalls(ball));
            }
        }

        private bool TryGetBallFromCollision(Collision2D collision2D, out Ball ball)
        {
            if(collision2D.collider.gameObject.TryGetComponent(out ball))
            {
                return true;
            }
            
            if(collision2D.otherCollider.gameObject.TryGetComponent(out ball))
            {
                return true;
            }

            return false;
        }

        private List<Ball> CreateBalls(Ball original)
        {
            var count = _ballsOnField.All.Count;
            var speed = original.GetSpeed();

            var result = new List<Ball>();
            
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
                result.Add(newBall);
            }

            return result;
        }
    }
}