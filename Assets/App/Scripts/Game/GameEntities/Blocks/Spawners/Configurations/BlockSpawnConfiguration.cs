using Game.GameEntities.Blocks.Configurations;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.Blocks.Spawners.Configurations
{
    public class BlockSpawnConfiguration : MonoBehaviour
    {
        [SerializeField] private BlockConfiguration _blockConfiguration;
        [SerializeField] private BehaviorObjectInstaller<Block> _blockBehaviorInstaller;
        
        public BlockConfiguration BlockConfiguration => _blockConfiguration;
        public BehaviorObjectInstaller<Block> BlockBehaviorInstaller => _blockBehaviorInstaller;
    }
}