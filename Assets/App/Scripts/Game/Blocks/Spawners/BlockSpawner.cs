using Common.Data.Models;
using Game.Blocks.Configurations;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Spawners
{
    public class BlockSpawner : IBlockSpawner
    {
        private readonly IObjectPool<Block> _blocksPool;
        private readonly BlockSpawnSystemConfiguration _blockSystemConfiguration;
        private readonly Transform _spawnTransform;

        public BlockSpawner(IPoolProvider poolProvider, BlockSpawnSystemConfiguration blockSystemConfiguration,
            Transform spawnTransform)
        {
            _blocksPool = poolProvider.GetPool<Block>();
            _blockSystemConfiguration = blockSystemConfiguration;
            _spawnTransform = spawnTransform;
        }
        
        public Block SpawnBlock(int blockId, BlockSpawnData blockSpawnData)
        {
            var blockSpawnConfiguration = _blockSystemConfiguration.FindBlockConfiguration(blockId);
            var blockConfiguration = blockSpawnConfiguration.BlockConfiguration;
            var blockBehaviorInstaller = blockSpawnConfiguration.BlockBehaviorInstaller;
            
            var block = _blocksPool.Get();

            block.transform.SetParent(_spawnTransform);
            block.Initialize(blockConfiguration);
            block.SetSize(blockSpawnData.Size);
            block.SetPosition(blockSpawnData.Position);

            blockBehaviorInstaller.InstallCollisionBehaviours(block);
            blockBehaviorInstaller.InstallDestroyBehaviours(block);
            
            return block;
        }
    }
}