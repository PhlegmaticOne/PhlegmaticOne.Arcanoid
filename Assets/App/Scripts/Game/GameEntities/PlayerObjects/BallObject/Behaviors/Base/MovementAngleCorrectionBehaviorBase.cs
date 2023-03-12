using System;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Base
{
    public abstract class MovementAngleCorrectionBehaviorBase : IObjectBehavior<Ball>
    {
        protected const float Tolerance = 0.01f;
        protected float MinTopBounceAngle;
        protected float MinSideBounceAngle;

        public void SetBehaviorParameters(float minTopBounceAngle, float minSideBounceAngle)
        {
            MinTopBounceAngle = minTopBounceAngle;
            MinSideBounceAngle = minSideBounceAngle;
        }

        public void Behave(Ball entity, Collision2D collision2D)
        {
            var normal = collision2D.contacts[0].normal;
            var ballVelocity = entity.GetSpeed();
            var velocityAngle = Vector3.Angle(ballVelocity, normal);
            
            if (IsLeftWall(normal) && SideAngleValid(velocityAngle))
            {
                ballVelocity = ResolveLeftWallBounce(ballVelocity);
                ReflectAndSetSpeed(entity, ballVelocity, normal);
            }
            
            if (IsRightWall(normal) && SideAngleValid(velocityAngle))
            {
                ballVelocity = ResolveRightWallBounce(ballVelocity);
                ReflectAndSetSpeed(entity, ballVelocity, normal);
            }
            
            if (IsTopWall(normal) && TopBottomAngleValid(velocityAngle))
            {
                ballVelocity = ResolveTopWallBounce(ballVelocity);
                ReflectAndSetSpeed(entity, ballVelocity, normal);
            }
            
            if (IsBottomWall(normal) && TopBottomAngleValid(velocityAngle))
            {
                ballVelocity = ResolveBottomWallBounce(ballVelocity);
                ReflectAndSetSpeed(entity, ballVelocity, normal);
            }
        }
        
        protected abstract bool SideAngleValid(in float angle);
        protected abstract bool TopBottomAngleValid(in float angle);

        protected virtual Vector2 ResolveLeftWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.y >= 0 ? MinSideBounceAngle : -MinSideBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        protected virtual Vector2 ResolveRightWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.y >= 0 ? -MinSideBounceAngle : MinSideBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        protected virtual Vector2 ResolveTopWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? MinTopBounceAngle : -MinTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }
        
        protected virtual Vector2 ResolveBottomWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? -MinTopBounceAngle : MinTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        protected static Vector2 Rotate(in Vector2 vector2, in float angle) =>
            Quaternion.Euler(0, 0, angle) * vector2;

        private static void ReflectAndSetSpeed(Ball entity, in Vector2 ballVelocity, in Vector2 normal) => 
            entity.SetSpeed(Vector3.Reflect(ballVelocity, normal));
        private static bool IsLeftWall(in Vector2 normal) => Math.Abs(normal.x - 1f) < Tolerance;
        private static bool IsRightWall(in Vector2 normal) => Math.Abs(normal.x - (-1f)) < Tolerance;
        protected abstract bool IsTopWall(in Vector2 normal);
        protected abstract bool IsBottomWall(in Vector2 normal);
    }
}