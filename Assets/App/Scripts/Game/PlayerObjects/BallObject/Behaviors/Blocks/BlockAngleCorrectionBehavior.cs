using Game.Behaviors;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Blocks
{
    public class BlockAngleCorrectionBehavior : IObjectBehavior<Ball>
    {
        private float _minSideBounceAngle;

        public void SetBehaviorParameters(float minSideBounceAngle)
        {
            _minSideBounceAngle = minSideBounceAngle;
        }
        
        public void Behave(Ball entity, Collision2D collision2D)
        {
            var normal = collision2D.contacts[0].normal;
            Debug.Log(normal);
            // var ballVelocity = entity.GetSpeed();
            // var velocityAngle = Vector3.Angle(ballVelocity, normal);
            //
            // if (IsLeftWall(normal) && SideAngleValid(velocityAngle))
            // {
            //     ballVelocity = ResolveLeftWallBounce(ballVelocity);
            //     ReflectAndSetSpeed(entity, ballVelocity, normal);
            // }
            //
            // if (IsRightWall(normal) && SideAngleValid(velocityAngle))
            // {
            //     ballVelocity = ResolveRightWallBounce(ballVelocity);
            //     ReflectAndSetSpeed(entity, ballVelocity, normal);
            // }
        }
        
        private bool SideAngleValid(in float angle) => 90f - angle < _minSideBounceAngle;

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
        
        private static Vector2 Rotate(in Vector2 vector2, in float angle) =>
            Quaternion.Euler(0, 0, angle) * vector2;
        private static void ReflectAndSetSpeed(Ball entity, in Vector2 ballVelocity, in Vector2 normal) => 
            entity.SetSpeed(Vector3.Reflect(ballVelocity, normal));
        private static bool IsLeftWall(in Vector2 normal) => normal.x == 1f;
        private static bool IsRightWall(in Vector2 normal) => normal.x == -1f;
    }
}