using System.Collections.Generic;
using Game.GameEntities.Blocks.Configurations;
using Libs.Behaviors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.ChainBomb
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create chain bomb configuration")]
    public class ChainBombConfiguration : ScriptableObject
    {
        [SerializeField] private BlockAffectingType _blockAffecting;
        
        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffectingType.Damage)]
        private int _removesLifesCount;

        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffectingType.Damage)]
        private List<BlockConfiguration> _damageAffectsOnBlocks;
        
        [SerializeField] [ShowIf(nameof(_blockAffecting), BlockAffectingType.Destroying)]
        private List<BlockConfiguration> _destroyAffectsOnBlocks;

        [SerializeField] private ColliderTag _colliderTag;

        public BlockAffectingType BlockAffecting => _blockAffecting;
        public int RemovesLifesCount => _removesLifesCount;
        public List<BlockConfiguration> DamageAffectsOnBlocks => _damageAffectsOnBlocks;
        public List<BlockConfiguration> DestroyAffectsOnBlocks => _destroyAffectsOnBlocks;
        public ColliderTag ColliderTag => _colliderTag;
    }
}