using Game.Behaviors;
using Game.Behaviors.Installer;

namespace Game.PlayerObjects.BallObject.Behaviours.Ship
{
    public class MovementAngleCorrectionBehaviourInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            return new MovementAngleCorrectionBehaviour();
        }
    }
}