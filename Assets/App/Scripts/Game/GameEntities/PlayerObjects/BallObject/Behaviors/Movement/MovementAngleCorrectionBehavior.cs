using System;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Movement
{
    public class MovementAngleCorrectionBehavior : IObjectBehavior<Ball>
    {
        private const float Tolerance = 0.01f;
        private float _minTopBottomBounceAngle;
        private float _minSideBounceAngle;
        private bool _isCorrectMovement;

        public void SetBehaviorParameters(float minTopBottomBounceAngle, float minSideBounceAngle, bool isCorrectMovement)
        {
            _minTopBottomBounceAngle = minTopBottomBounceAngle;
            _minSideBounceAngle = minSideBounceAngle;
            _isCorrectMovement = isCorrectMovement;
        }

        public void Behave(Ball entity, Collision2D collision2D)
        {
            if (_isCorrectMovement == false)
            {
                return;
            }
            
            var normal = collision2D.contacts[0].normal;
            var ballVelocity = entity.GetSpeed();
            var velocityAngle = Vector3.Angle(ballVelocity, normal);

            var resolvedBounce = Vector2.zero;
            
            if (IsNormalDown(normal))
            {
                resolvedBounce = ResolveBottomWallBounce(ballVelocity, velocityAngle);
            }
            else if (IsNormalUp(normal))
            {
                resolvedBounce = ResolveTopWallBounce(ballVelocity, velocityAngle);
            }
            else if (IsNormalLeft(normal))
            {
                resolvedBounce = ResolveLeftWallBounce(ballVelocity, velocityAngle);
            }
            else if (IsNormalRight(normal))
            {
                resolvedBounce = ResolveRightWallBounce(ballVelocity, velocityAngle);
            }
            
            entity.SetSpeed(resolvedBounce == Vector2.zero ? ballVelocity : resolvedBounce);
        }

        private Vector2 ResolveBottomWallBounce(in Vector2 ballVelocity, in float velocityAngle) => 
            ResolveBounce(ballVelocity, velocityAngle, _minTopBottomBounceAngle, ballVelocity.x, 1);

        private Vector2 ResolveTopWallBounce(in Vector2 ballVelocity, in float velocityAngle) => 
            ResolveBounce(ballVelocity, velocityAngle, _minTopBottomBounceAngle, ballVelocity.x, -1);
        
        private Vector2 ResolveLeftWallBounce(in Vector2 ballVelocity, in float velocityAngle) => 
            ResolveBounce(ballVelocity, velocityAngle, _minSideBounceAngle, ballVelocity.y, -1);

        private Vector2 ResolveRightWallBounce(in Vector2 ballVelocity, in float velocityAngle) => 
            ResolveBounce(ballVelocity, velocityAngle, _minSideBounceAngle, ballVelocity.y, 1);
        
        private static Vector2 ResolveBounce(in Vector2 ballVelocity, in float velocityAngle,
            in float comparingAngle, in float axis, in int multiplier)
        {
            var angle = 0f;

            if (velocityAngle < comparingAngle)
            {
                angle = axis >= 0 ? multiplier * comparingAngle : -multiplier * comparingAngle;
            }
            
            if (velocityAngle > 90 - comparingAngle)
            {
                angle = axis >= 0 ? -multiplier * comparingAngle : multiplier * comparingAngle;
            }
            
            return Rotate(ballVelocity, angle);
        }

        private static Vector2 Rotate(in Vector2 vector2, in float angle) => Quaternion.Euler(0, 0, angle) * vector2;
        private static bool IsNormalLeft(in Vector2 normal) => Math.Abs(normal.x - (-1f)) < Tolerance;
        private static bool IsNormalRight(in Vector2 normal) => Math.Abs(normal.x - 1f) < Tolerance;
        private static bool IsNormalUp(in Vector2 normal) => Math.Abs(normal.y - 1) < Tolerance;
        private static bool IsNormalDown(in Vector2 normal) => Math.Abs(normal.y - (-1)) < Tolerance;
    }
}