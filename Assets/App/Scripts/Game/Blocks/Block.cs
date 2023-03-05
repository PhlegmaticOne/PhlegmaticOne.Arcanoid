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
        private int _health;
        private bool _readyToDestroy;

        public BlockView BlockView => _blockView;
        public int StartHealth { get; private set; }
        public int CurrentHealth => _health;
        public BlockConfiguration BlockConfiguration { get; private set; }
        
        public void Initialize(BlockConfiguration configuration, int health)
        {
            BlockConfiguration = configuration;
            _health = health;
            StartHealth = health;
            _blockView.SetMainSprite(configuration.BlockSprite);
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

        public float GetBaseHeight() => _boxCollider.size.y;

        protected override bool CanBeDestroyedOnDestroyCollision() => _readyToDestroy;

        protected override void ResetProtected()
        {
            _blockView.Reset();
            _health = 0;
            StartHealth = 0;
            _readyToDestroy = false;
            BlockConfiguration = null;
        }
    }
}