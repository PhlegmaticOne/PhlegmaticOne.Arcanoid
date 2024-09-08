﻿using System.Collections.Generic;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallBehavior : IObjectBehavior<Block>
    {
        private const float DeltaAngle = 2f;
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
            _captiveBallsSystem.AddNewBalls(TryGetBallFromCollision(collision2D, out var ball)
                ? CreateBalls(ball.transform)
                : CreateBalls(entity.transform));
        }
        
        public bool IsDefault => false;

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

        private List<Ball> CreateBalls(Transform spawnTransform)
        {
            var mainBall = _ballsOnField.All[0];
            var count = _ballsOnField.All.Count;
            var speed = mainBall.CurrentSpeed;
            var result = new List<Ball>();

            for (var i = 0; i < count; i++)
            {
                var newBall = _ballSpawner.CreateBall(new BallCreationContext
                {
                    Position = spawnTransform.transform.position,
                    StartSpeed = speed,
                    SetSpecifiedStartSpeed = true
                });

                var direction = Quaternion.Euler(0, 0, DeltaAngle * i * Sign(i) / 2.0f) * Vector2.down;
                newBall.StartMove(direction);
                mainBall.CopyToBall(newBall);
                _ballsOnField.Add(newBall);
                result.Add(newBall);
            }

            return result;
        }

        private static int Sign(int index) => index % 2 == 0 ? 1 : -1;
    }
}