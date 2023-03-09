using Game.Behaviors;
using Game.Blocks.Configurations;
using Game.Blocks.View;
using UnityEngine;

namespace Game.Blocks
{
    public class Block : BehaviorObject<Block>
    {
        [SerializeField] private BlockView _blockView;
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private int _health;
        private bool _readyToDestroy;

        public BlockView BlockView => _blockView;
        public int StartHealth { get; private set; }
        public int CurrentHealth => _health;
        public bool IsDestroyed { get; set; }
        public BlockConfiguration BlockConfiguration { get; private set; }
        
        public void Initialize(BlockConfiguration configuration)
        {
            BlockConfiguration = configuration;
            _health = configuration.LifesCount;
            StartHealth = configuration.LifesCount;
            IsDestroyed = false;
            
            _blockView.SetMainSprite(configuration.BlockSprite);
            foreach (var additionalSprite in configuration.AdditionalSprites)
            {
                _blockView.AddSprite(additionalSprite, true);
            }

            if (configuration.Gravitable)
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetSize(Vector2 size)
        {
            _blockView.SetSize(size);
            _boxCollider.size = _blockView.Size;
        }

        public void Damage()
        {
            --_health;
            
            if (_health == 1)
            {
                _readyToDestroy = true;
            }
        }

        public Vector2 GetBaseSize() => _boxCollider.size;

        protected override bool CanBeDestroyedOnDestroyCollision() => _readyToDestroy;

        protected override void ResetProtected()
        {
            _blockView.Reset();
            _health = 0;
            StartHealth = 0;
            _readyToDestroy = false;
            IsDestroyed = true;
            BlockConfiguration = null;
        }
    }
}