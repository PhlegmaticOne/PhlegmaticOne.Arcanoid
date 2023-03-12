using System.Collections.Generic;
using Game.Behaviors;
using Game.Blocks.Configurations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Blocks.Behaviors.Bomb
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create bomb configuration")]
    public class BombBlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _destroyingRadius;
        [SerializeField] private bool _damagesBlocks;
        
        [SerializeField] [ShowIf(nameof(_damagesBlocks))]
        private int _removesLifesCount;

        [SerializeField] [ShowIf(nameof(_damagesBlocks))]
        private List<BlockConfiguration> _damageAffectsOnBlocks;

        [SerializeField] private bool _destroysBlocks;

        [SerializeField] [ShowIf(nameof(_destroysBlocks))]
        private List<BlockConfiguration> _destroyAffectsOnBlocks;

        [SerializeField] private ColliderTag _colliderTag;

        public int DestroyingRadius => _destroyingRadius;
        public bool DamagesBlocks => _damagesBlocks;
        public int RemovesLifesCount => _removesLifesCount;
        public List<BlockConfiguration> DamageAffectsOnBlocks => _damageAffectsOnBlocks;
        public bool DestroysBlocks => _destroysBlocks;
        public List<BlockConfiguration> DestroyAffectsOnBlocks => _destroyAffectsOnBlocks;
        public ColliderTag ColliderTag => _colliderTag;
    }
}