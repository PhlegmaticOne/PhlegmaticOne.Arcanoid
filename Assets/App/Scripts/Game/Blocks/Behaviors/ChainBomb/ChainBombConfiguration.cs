using System.Collections.Generic;
using Game.Behaviors;
using Game.Blocks.Configurations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Blocks.Behaviors.ChainBomb
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create chain bomb configuration")]
    public class ChainBombConfiguration : ScriptableObject
    {
        [SerializeField] private BlockAffecting _blockAffecting;
        
        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffecting.Damage)]
        private int _removesLifesCount;

        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffecting.Damage)]
        private List<BlockConfiguration> _damageAffectsOnBlocks;
        
        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffecting.Destroying)]
        private List<BlockConfiguration> _destroyAffectsOnBlocks;

        [SerializeField] private ColliderTag _colliderTag;

        public BlockAffecting BlockAffecting => _blockAffecting;
        public int RemovesLifesCount => _removesLifesCount;
        public List<BlockConfiguration> DamageAffectsOnBlocks => _damageAffectsOnBlocks;
        public List<BlockConfiguration> DestroyAffectsOnBlocks => _destroyAffectsOnBlocks;
        public ColliderTag ColliderTag => _colliderTag;
    }
}