using UnityEngine;

namespace Game.GameEntities.Blocks.View
{
    public class AdditionalRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Vector2 _blockSize;
        private bool _preserveOriginalSize;

        public void Initialize(Sprite sprite, Vector2 size, int sortingOrder, bool preserveOriginalSize = false)
        {
            _preserveOriginalSize = preserveOriginalSize;
            _blockSize = size;
            _spriteRenderer.sortingOrder = sortingOrder;
            SetSprite(sprite);
            
            if (preserveOriginalSize == false)
            {
                SetSize(size);
            }
        }
        
        public void SetSize(Vector2 size)
        {
            var ratio = size.x / _blockSize.x;

            if (ratio < 1)
            {
                _spriteRenderer.size *= ratio;
            }
            else if(_preserveOriginalSize == false)
            {
                _spriteRenderer.size = size;
            }
        }
        
        private void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
    }
}