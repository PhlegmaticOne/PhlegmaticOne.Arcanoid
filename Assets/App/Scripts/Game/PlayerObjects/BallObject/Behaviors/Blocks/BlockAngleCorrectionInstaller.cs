using Game.Behaviors;
using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Blocks
{
    public class BlockAngleCorrectionInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private float _minSideAngle;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behavior = new BlockAngleCorrectionBehavior();
            behavior.SetBehaviorParameters(_minSideAngle);
            return behavior;
        }
    }
}