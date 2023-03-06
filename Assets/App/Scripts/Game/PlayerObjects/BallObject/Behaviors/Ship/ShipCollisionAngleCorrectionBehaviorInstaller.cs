using Game.Behaviors;
using Game.Behaviors.Installer;

namespace Game.PlayerObjects.BallObject.Behaviors.Ship
{
    public class ShipCollisionAngleCorrectionBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            return new ShipCollisionAngleCorrectionBehavior();
        }
    }
}