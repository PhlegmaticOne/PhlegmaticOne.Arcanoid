using System.Collections.Generic;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Chain
{
    [CreateAssetMenu(menuName = "Game/Bombs/Behaviors/Create chain bomb configuration", order = 0)]
    public class ChainBombConfiguration : BombConfiguration
    {
        [SerializeField] private List<UnderlyingBlockConfiguration> _exclude;
        public override BlockAffectingType GetAffectingType(BlockConfiguration blockConfiguration)
        {
            if (blockConfiguration.HasUnderlyingConfiguration)
            {
                if (_exclude.Contains(blockConfiguration.UnderlyingBlockConfiguration))
                {
                    return BlockAffectingType.None;
                }

                return _blockAffecting;
            }

            return BlockAffectingType.None;
        }
    }
}