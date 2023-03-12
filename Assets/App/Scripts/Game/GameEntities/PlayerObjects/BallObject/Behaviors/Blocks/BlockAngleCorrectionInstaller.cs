using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Blocks
{
    public class BlockAngleCorrectionInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private float _minSideAngle;
        [SerializeField] private float _minTopBottomAngle;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behavior = new BlockAngleCorrectionBehavior();
            behavior.SetBehaviorParameters(_minSideAngle, _minTopBottomAngle);
            return behavior;
        }
    }
}