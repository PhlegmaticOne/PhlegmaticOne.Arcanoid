using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Movement
{
    public class MovementAngleCorrectionBehaviorInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private float _minSideAngle;
        [SerializeField] private float _minTopBottomAngle;
        [SerializeField] private bool _isCorrectMovement = true;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behavior = new MovementAngleCorrectionBehavior();
            behavior.SetBehaviorParameters(_minTopBottomAngle, _minSideAngle, _isCorrectMovement);
            return behavior;
        }
    }
}