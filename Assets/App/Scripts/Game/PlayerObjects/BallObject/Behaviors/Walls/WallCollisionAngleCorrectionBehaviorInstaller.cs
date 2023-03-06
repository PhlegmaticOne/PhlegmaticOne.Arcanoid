using Game.Behaviors;
using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Walls
{
    public class WallCollisionAngleCorrectionBehaviorInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private float _minTopBounceAngle;
        [SerializeField] private float _minSideBounceAngle;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behavior = new WallCollisionAngleCorrectionBehavior();
            behavior.SetBehaviorParameters(_minTopBounceAngle,_minSideBounceAngle);
            return behavior;
        }
    }
}