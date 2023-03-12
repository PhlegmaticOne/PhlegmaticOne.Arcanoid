using Libs.Behaviors;
using Libs.Behaviors.Installer;

namespace Game.GameEntities.Blocks.Behaviors.Common.BallDamage
{
    public class BallDamageBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var behavior = new BallDamageBehavior();
            return behavior;
        }
    }
}