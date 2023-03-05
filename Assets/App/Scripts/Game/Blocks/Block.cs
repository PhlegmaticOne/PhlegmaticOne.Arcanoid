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
        public BlockConfiguration BlockConfiguration { get; private set; }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetSize(Vector2 size)
        {
            _blockView.SetSize(size);
            _boxCollider.size = _blockView.Size;
        }

        public void Initialize(BlockConfiguration configuration)
        {
            BlockConfiguration = configuration;
            _blockView.SetSprite(configuration.BlockSprite);
        }

        public float GetBaseHeight() => _boxCollider.size.y;

        protected override bool CanBeDestroyedOnDestroyCollision()
        {
            return false;
        }

        protected override void ResetProtected()
        {
            base.ResetProtected();
        }
    }
}