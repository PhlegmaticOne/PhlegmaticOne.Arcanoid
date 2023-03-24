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
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private float _health;
        private int _previousHealth;
        private bool _readyToDestroy;
        
        public int StartHealth { get; private set; }
        public float CurrentHealth => _health;
        public bool IsDestroyed { get; private set; }
        public bool IsActive { get; private set; }
        public BlockConfiguration BlockConfiguration { get; private set; }
        
        public void Initialize(BlockConfiguration configuration, BlockCracksConfiguration blockCracksConfiguration)
        {
            BlockConfiguration = configuration;
            IsActive = configuration.ActiveOnPlay;
            _health = configuration.LifesCount;
            StartHealth = configuration.LifesCount;
            IsDestroyed = false;
            _previousHealth = StartHealth;
            _blockView.Initialize(configuration, blockCracksConfiguration);
        }

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

        public bool IsDefaultBlock() => BlockConfiguration.ActiveOnPlay && IsDestroyed == false;

        public Vector2 GetBaseSize() => _boxCollider.size;

        protected override bool CanBeDestroyedOnDestroyCollision() => _readyToDestroy;

        protected override void ResetProtected()
        {
            _blockView.Reset();
            _health = 0;
            StartHealth = 0;
            _readyToDestroy = false;
            IsDestroyed = true;
        }
    }
}