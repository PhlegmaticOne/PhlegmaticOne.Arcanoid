using Game.Blocks.Behaviors.Common.BallDamage;
using Game.Blocks.Configurations;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks.Spawners
{
    public class BlockSpawner : IBlockSpawner
    {
        private readonly IObjectPool<Block> _blocksPool;
        private readonly BlockSpawnSystemConfiguration _blockSystemConfiguration;
        private readonly BlockCracksConfiguration _blockCracksConfiguration;
        private readonly Transform _spawnTransform;

        public BlockSpawner(IPoolProvider poolProvider,
            BlockSpawnSystemConfiguration blockSystemConfiguration,
            BlockCracksConfiguration blockCracksConfiguration,
            Transform spawnTransform)
        {
            _blocksPool = poolProvider.GetPool<Block>();
            _blockSystemConfiguration = blockSystemConfiguration;
            _blockCracksConfiguration = blockCracksConfiguration;
            _spawnTransform = spawnTransform;
        }
        
        public Block SpawnBlock(int blockId, BlockSpawnData blockSpawnData)
        {
            if (blockId == _blockSystemConfiguration.NoneBlockId)
            {
                return null;
            }
            
            var blockSpawnConfiguration = _blockSystemConfiguration.FindBlockConfiguration(blockId);
            var blockConfiguration = blockSpawnConfiguration.BlockConfiguration;
            var blockBehaviorInstaller = blockSpawnConfiguration.BlockBehaviorInstaller;
            
            var block = _blocksPool.Get();

            block.transform.SetParent(_spawnTransform);
            block.Initialize(blockConfiguration, _blockCracksConfiguration);
            block.SetSize(blockSpawnData.Size);
            block.SetPosition(blockSpawnData.Position);

            blockBehaviorInstaller.InstallCollisionBehaviours(block);
            blockBehaviorInstaller.InstallDestroyBehaviours(block);
            
            return block;
        }
    }
}