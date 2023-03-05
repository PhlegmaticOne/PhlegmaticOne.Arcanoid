using Game.Blocks.Configurations;
using Libs.Pooling;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Spawners
{
    public class BlockSpawnerInstaller : MonoBehaviour
    {
        [SerializeField] private BlockSpawnSystemConfiguration _blockSystemConfiguration;
        [SerializeField] private UnityObjectPoolInstaller<Block> _blockPoolInstaller;

        public UnityObjectPoolInstaller<Block> BlockPoolInstaller => _blockPoolInstaller;

        public IBlockSpawner CreateBlockSpawner(IPoolProvider poolProvider)
        {
            return new BlockSpawner(poolProvider, _blockSystemConfiguration);
        }
    }
}