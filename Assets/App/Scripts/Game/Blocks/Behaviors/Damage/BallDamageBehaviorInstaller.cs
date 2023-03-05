using Game.Behaviors;
using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.Blocks.Behaviors.Damage
{
    public class BallDamageBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private BlockCracksConfiguration _blockCracksConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var behavior = new BallDamageBehavior();
            behavior.SetBehaviourParameters(_blockCracksConfiguration);
            return behavior;
        }
    }
}