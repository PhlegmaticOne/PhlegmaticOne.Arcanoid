using UnityEngine;

namespace Game.Blocks.View
{
    public class AdditionalRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite, Vector2 size, int sortingOrder, bool preserveOriginalSize = false)
        {
            _spriteRenderer.sortingOrder = sortingOrder;
            SetSprite(sprite);
            
            if (preserveOriginalSize == false)
            {
                SetSize(size);
            }
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetSize(Vector2 size)
        {
            _spriteRenderer.size = size;
        }
    }
}