using Common.Data.Models;
using Game.Blocks.Configurations;
using Libs.Pooling.Base;

namespace Game.Blocks.Spawners
{
    public class BlockSpawner : IBlockSpawner
    {
        private readonly IObjectPool<Block> _blocksPool;
        private readonly BlockSpawnSystemConfiguration _blockSystemConfiguration;

        public BlockSpawner(IPoolProvider poolProvider, BlockSpawnSystemConfiguration blockSystemConfiguration)
        {
            _blocksPool = poolProvider.GetPool<Block>();
            _blockSystemConfiguration = blockSystemConfiguration;
        }
        
        public Block SpawnBlock(BlockData blockData, BlockSpawnData blockSpawnData)
        {
            var blockSpawnConfiguration = _blockSystemConfiguration.FindBlockConfiguration(blockData);
            var blockConfiguration = blockSpawnConfiguration.BlockConfiguration;
            var blockBehaviorInstaller = blockSpawnConfiguration.BlockBehaviorInstaller;
            
            var block = _blocksPool.Get();
            
            block.Initialize(blockConfiguration, blockData.LifesCount);
            block.SetSize(blockSpawnData.Size);
            block.SetPosition(blockSpawnData.Position);

            blockBehaviorInstaller.InstallCollisionBehaviours(block);
            blockBehaviorInstaller.InstallDestroyBehaviours(block);
            
            return block;
        }
    }
}