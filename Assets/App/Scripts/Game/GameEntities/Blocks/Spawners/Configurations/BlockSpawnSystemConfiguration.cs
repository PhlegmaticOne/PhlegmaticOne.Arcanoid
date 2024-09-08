using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameEntities.Blocks.Spawners.Configurations
{
    public class BlockSpawnSystemConfiguration : MonoBehaviour
    {
        [SerializeField] private List<BlockSpawnConfiguration> _blockConfigurations;
        [SerializeField] private int _noneBlockId = -1;
        
        public List<BlockSpawnConfiguration> BlockConfigurations => _blockConfigurations;
        public int NoneBlockId => _noneBlockId;

        public BlockSpawnConfiguration FindBlockConfiguration(int blockId)
        {
            var configuration = _blockConfigurations.FirstOrDefault(x => x.BlockConfiguration.BlockId == blockId);
            return configuration;
        }
    }
}