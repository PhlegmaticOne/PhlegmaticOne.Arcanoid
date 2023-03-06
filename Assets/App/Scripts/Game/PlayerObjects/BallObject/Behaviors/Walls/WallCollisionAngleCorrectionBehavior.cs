using Game.Behaviors;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Walls
{
    public class WallCollisionAngleCorrectionBehavior : IObjectBehavior<Ball>
    {
        private float _minTopBounceAngle;
        private float _minSideBounceAngle;

        public void SetBehaviorParameters(float minTopBounceAngle, float minSideBounceAngle)
        {
            _minTopBounceAngle = minTopBounceAngle;
            _minSideBounceAngle = minSideBounceAngle;
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
        
        private bool SideAngleValid(in float angle) => 90f - angle < _minSideBounceAngle;
        private bool TopBottomAngleValid(in float angle) => angle < _minTopBounceAngle;

        private Vector2 ResolveLeftWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.y >= 0 ? _minSideBounceAngle : -_minSideBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        private Vector2 ResolveRightWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.y >= 0 ? -_minSideBounceAngle : _minSideBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        private Vector2 ResolveTopWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? _minTopBounceAngle : -_minTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }
        
        private Vector2 ResolveBottomWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? -_minTopBounceAngle : _minTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        private static Vector2 Rotate(in Vector2 vector2, in float angle) =>
            Quaternion.Euler(0, 0, angle) * vector2;
        private static void ReflectAndSetSpeed(Ball entity, in Vector2 ballVelocity, in Vector2 normal) => 
            entity.SetSpeed(Vector3.Reflect(ballVelocity, normal));
        private static bool IsLeftWall(in Vector2 normal) => normal.x == 1f;
        private static bool IsRightWall(in Vector2 normal) => normal.x == -1f;
        private static bool IsTopWall(in Vector2 normal) => normal.y == -1f;
        private static bool IsBottomWall(in Vector2 normal) => normal.y == 1f;
    }
}