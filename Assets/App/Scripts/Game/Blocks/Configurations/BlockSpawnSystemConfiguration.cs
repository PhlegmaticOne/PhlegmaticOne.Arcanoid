using System.Collections.Generic;
using System.Linq;
using Common.Data.Models;
using UnityEngine;

namespace Game.Blocks.Configurations
{
    public class BlockSpawnSystemConfiguration : MonoBehaviour
    {
        [SerializeField] private List<BlockSpawnConfiguration> _blockConfigurations;
        
        public List<BlockSpawnConfiguration> BlockConfigurations => _blockConfigurations;

        public BlockSpawnConfiguration FindBlockConfiguration(int blockId)
        {
            var configuration = _blockConfigurations.FirstOrDefault(x => x.BlockConfiguration.BlockId == blockId);
            return configuration;
        }
    }
}