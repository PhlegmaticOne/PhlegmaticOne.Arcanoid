using Game.GameEntities.Common;
using Libs.Behaviors;
using Libs.Behaviors.Installer;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Damage
{
    public class BallBlockDamageBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            return new DamageBlockBehavior<Ball>();
        }
    }
}