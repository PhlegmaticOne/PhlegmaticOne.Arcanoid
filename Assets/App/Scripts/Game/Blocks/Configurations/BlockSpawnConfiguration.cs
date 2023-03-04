using System.Collections.Generic;
using Game.Blocks.CollisionBehaviours.Base;
using UnityEngine;

namespace Game.Blocks.Configurations
{
    public class BlockSpawnConfiguration : MonoBehaviour
    {
        [SerializeField] private BlockConfiguration _blockConfiguration;
        [SerializeField] private List<CollisionBehaviourInstaller> _collisionBehaviourInstallers;

        public BlockConfiguration BlockConfiguration => _blockConfiguration;

        public List<CollisionBehaviourInstaller> CollisionBehaviourInstallers => _collisionBehaviourInstallers;
    }
}