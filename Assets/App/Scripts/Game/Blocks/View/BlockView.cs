using UnityEngine;

namespace Game.Blocks.View
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public float BaseWorldHeight => _spriteRenderer.bounds.size.y;
        public float BaseWorldWidth => _spriteRenderer.bounds.size.x;

        public void SetSize(Vector2 newSize)
        {
            _spriteRenderer.size = newSize;
        }
    }
}