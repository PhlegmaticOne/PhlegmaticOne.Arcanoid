using System;
using Game.PlayerObjects.BallObject.Behaviors.Base;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Blocks
{
    public class BlockAngleCorrectionBehavior : MovementAngleCorrectionBehaviorBase
    {
        protected override bool SideAngleValid(in float angle) => 90f - angle < MinSideBounceAngle;
        protected override bool TopBottomAngleValid(in float angle) => 90f - angle < MinTopBounceAngle;

        protected override Vector2 ResolveBottomWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? MinTopBounceAngle : -MinTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }

        protected override bool IsTopWall(in Vector2 normal) => Math.Abs(normal.y - 1) < Tolerance;

        protected override bool IsBottomWall(in Vector2 normal) => Math.Abs(normal.y - (-1)) < Tolerance;

        protected override Vector2 ResolveTopWallBounce(in Vector2 ballVelocity)
        {
            var angle = ballVelocity.x >= 0 ? -MinTopBounceAngle : MinTopBounceAngle;
            return Rotate(ballVelocity, angle);
        }
    }
}