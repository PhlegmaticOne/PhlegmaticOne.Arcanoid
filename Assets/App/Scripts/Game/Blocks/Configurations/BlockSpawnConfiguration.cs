using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.Blocks.Configurations
{
    public class BlockSpawnConfiguration : MonoBehaviour
    {
        [SerializeField] private BlockConfiguration _blockConfiguration;
        [SerializeField] private BehaviorObjectInstaller<Block> _blockBehaviorInstaller;
        
        public BlockConfiguration BlockConfiguration => _blockConfiguration;
        public BehaviorObjectInstaller<Block> BlockBehaviorInstaller => _blockBehaviorInstaller;
    }
}