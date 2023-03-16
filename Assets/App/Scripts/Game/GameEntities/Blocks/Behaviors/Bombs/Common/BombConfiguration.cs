using System.Collections.Generic;
using Game.GameEntities.Blocks.Behaviors.ChainBomb;
using Game.GameEntities.Blocks.Configurations;
using Libs.Behaviors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create bomb configuration")]
    public class BombConfiguration : ScriptableObject
    {
        [SerializeField] private BlockAffectingType _blockAffecting;
        [SerializeField] private bool _isAffectsOnAllBlocks;
        
        [SerializeField] 
        [ShowIf("@(this._blockAffecting == BlockAffectingType.Damage || " +
                "this._blockAffecting == BlockAffectingType.BothAndSeparate) && " +
                "this._isAffectsOnAllBlocks == false")]
        private int _removesLifesCount;

        [SerializeField] 
        [ShowIf("@(this._blockAffecting == BlockAffectingType.Damage || " +
                "this._blockAffecting == BlockAffectingType.BothAndSeparate) && " +
                "this._isAffectsOnAllBlocks == false")]
        private List<BlockConfiguration> _damageAffectsOnBlocks;
        
        [SerializeField] 
        [ShowIf("@(this._blockAffecting == BlockAffectingType.Destroying || " +
                "this._blockAffecting == BlockAffectingType.BothAndSeparate) && " +
                "this._isAffectsOnAllBlocks == false")]
        private List<BlockConfiguration> _destroyAffectsOnBlocks;

        [SerializeField] private ColliderTag _colliderTag;

        public bool IsAffectsOnAllBlocks => _isAffectsOnAllBlocks;
        public BlockAffectingType BlockAffecting => _blockAffecting;
        public int RemovesLifesCount => _removesLifesCount;
        public List<BlockConfiguration> DamageAffectsOnBlocks => _damageAffectsOnBlocks;
        public List<BlockConfiguration> DestroyAffectsOnBlocks => _destroyAffectsOnBlocks;
        public ColliderTag ColliderTag => _colliderTag;
    }
}