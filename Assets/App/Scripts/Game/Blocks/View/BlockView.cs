using UnityEngine;

namespace Game.Blocks.View
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public Vector2 Size => _spriteRenderer.size;

        public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        public void SetSize(Vector2 newSize) => _spriteRenderer.size = newSize;
    }
}