using Libs.Behaviors;
using Libs.Behaviors.Installer;

namespace Game.GameEntities.Blocks.Behaviors.Common.Damage
{
    public class GetDamageBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var behavior = new GetDamageBehavior();
            return behavior;
        }
    }
}