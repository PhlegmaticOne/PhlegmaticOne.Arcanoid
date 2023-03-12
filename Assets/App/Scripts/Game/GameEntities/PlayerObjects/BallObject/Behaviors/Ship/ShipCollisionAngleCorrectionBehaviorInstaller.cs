using Libs.Behaviors;
using Libs.Behaviors.Installer;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Ship
{
    public class ShipCollisionAngleCorrectionBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            return new ShipCollisionAngleCorrectionBehavior();
        }
    }
}