using Game.Behaviors;
using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.BallDamage
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