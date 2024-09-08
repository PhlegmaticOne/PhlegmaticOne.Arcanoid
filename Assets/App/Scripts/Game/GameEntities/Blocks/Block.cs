using DG.Tweening;
using Game.GameEntities.Blocks.Configurations;
using Game.GameEntities.Blocks.View;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks
{
    public class Block : BehaviorObject<Block>
    {
        [SerializeField] private BlockView _blockView;
        [SerializeField] private BoxCollider2D _boxCollider;
        private int _previousHealth;
        private float _health;
        private bool _readyToDestroy;
        
        public float CurrentHealth => _health;
        public bool IsDestroyed { get; private set; }
        public bool IsActive { get; private set; }
        public BlockConfiguration BlockConfiguration { get; private set; }
        
        public void Initialize(BlockConfiguration configuration, BlockCracksConfiguration blockCracksConfiguration)
        {
            _boxCollider.enabled = true;
            BlockConfiguration = configuration;
            _health = configuration.LifesCount;
            _previousHealth = configuration.LifesCount;
            IsActive = configuration.IsActive;
            IsDestroyed = false;
            _blockView.Initialize(configuration, blockCracksConfiguration);
        }

        public bool TryGetUnderlyingId(out int id)
        {
            id = -1;

            if (BlockConfiguration.HasUnderlyingConfiguration == false)
            {
                return false;
            }

            var underlyingConfiguration = BlockConfiguration.UnderlyingBlockConfiguration;
            id = underlyingConfiguration.Id;
            return true;
        }

        public void Disable()
        {
            IsDestroyed = true;
            _boxCollider.enabled = false;
        }
        
        public int GetUnderlyingId() => BlockConfiguration.UnderlyingBlockConfiguration.Id;

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetSize(Vector2 size)
        {
            _blockView.SetSize(size);
            _boxCollider.size = _blockView.Size;
        }

        public void Damage(float damage)
        {
            if (IsActive == false)
            {
                return;
            }
            
            _health -= damage;
            var rounded = (int)_health;
            
            if (_health > 0 && rounded != _previousHealth)
            {
                _previousHealth = rounded;
                _blockView.Damage(rounded);
            }
        }

        public bool IsDefaultBlock() => BlockConfiguration.IsActive && IsDestroyed == false;

        public Vector2 GetBaseSize() => _boxCollider.size;

        protected override bool CanBeDestroyedOnDestroyCollision() => _readyToDestroy;

        protected override void ResetProtected()
        {
            transform.localScale = Vector3.one;
            transform.DOKill();
            _blockView.Reset();
            _health = 0;
            _readyToDestroy = false;
            IsDestroyed = true;
        }
    }
}