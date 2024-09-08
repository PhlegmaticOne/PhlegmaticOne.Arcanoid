using System.Collections.Generic;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Default
{
    [CreateAssetMenu(menuName = "Game/Bombs/Behaviors/Create default bomb configuration", order = 0)]
    public class DefaultBombConfiguration : BombConfiguration
    {
        [SerializeField] private List<BlockConfiguration> _destroyingBlocks;
        
        public override BlockAffectingType GetAffectingType(BlockConfiguration blockConfiguration)
        {
            if (_destroyingBlocks.Contains(blockConfiguration))
            {
                return BlockAffectingType.Destroying;
            }

            return _blockAffecting;
        }
    }
}