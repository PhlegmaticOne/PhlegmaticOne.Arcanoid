using Game.Blocks.Configurations;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Spawners
{
    public class BlockSpawnerInitializer : MonoBehaviour
    {
        [SerializeField] private BlockSpawnSystemConfiguration _blockSystemConfiguration;

        public IBlockSpawner CreateBlockSpawner(IPoolProvider poolProvider)
        {
            return new BlockSpawner(poolProvider, _blockSystemConfiguration);
        }
    }
}