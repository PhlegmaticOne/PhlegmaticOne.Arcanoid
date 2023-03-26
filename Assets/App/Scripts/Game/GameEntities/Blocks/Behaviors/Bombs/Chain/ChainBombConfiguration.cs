using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Chain
{
    [CreateAssetMenu(menuName = "Game/Bombs/Behaviors/Create chain bomb configuration", order = 0)]
    public class ChainBombConfiguration : BombConfiguration
    {
        public override BlockAffectingType GetAffectingType(BlockConfiguration blockConfiguration)
        {
            return blockConfiguration.HasUnderlyingConfiguration ? _blockAffecting : BlockAffectingType.None;
        }
    }
}