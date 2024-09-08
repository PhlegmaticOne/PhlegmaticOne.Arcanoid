using Game.GameEntities.Blocks.Configurations;
using Libs.Behaviors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    public abstract class BombConfiguration : ScriptableObject
    {
        [SerializeField] private ColliderTag _colliderTag;
        [SerializeField] protected BlockAffectingType _blockAffecting;
        
        [SerializeField]
        [ShowIf("@(this._blockAffecting == BlockAffectingType.Damage || " +
                 "this._blockAffecting == BlockAffectingType.Custom)")]
        private float _damage;
        public float Damage => _damage;
        public ColliderTag ColliderTag => _colliderTag;
        public abstract BlockAffectingType GetAffectingType(BlockConfiguration blockConfiguration);
    }
}