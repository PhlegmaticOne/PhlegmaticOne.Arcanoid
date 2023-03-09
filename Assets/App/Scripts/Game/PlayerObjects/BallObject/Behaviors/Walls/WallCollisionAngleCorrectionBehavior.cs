using System;
using Game.PlayerObjects.BallObject.Behaviors.Base;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Walls
{
    public class WallCollisionAngleCorrectionBehavior : MovementAngleCorrectionBehaviorBase
    {
        protected override bool SideAngleValid(in float angle) => 90f - angle < MinSideBounceAngle;
        protected override bool TopBottomAngleValid(in float angle) => angle < MinTopBounceAngle;
        
        protected override bool IsTopWall(in Vector2 normal) => Math.Abs(normal.y - (-1)) < Tolerance;

        protected override bool IsBottomWall(in Vector2 normal) => Math.Abs(normal.y - 1) < Tolerance;
    }
}