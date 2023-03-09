using System.Collections.Generic;
using UnityEngine;

namespace Game.Blocks.View
{
    public class BlockView : MonoBehaviour
    {
        private int _currentSortOrder;
        
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private AdditionalRenderer _additionalRenderer;
        
        private readonly Stack<AdditionalRenderer> _additionalRenderers = new Stack<AdditionalRenderer>();
        public Vector2 Size => _mainSpriteRenderer.size;

        public void SetMainSprite(Sprite sprite)
        {
            _mainSpriteRenderer.sprite = sprite;
            _currentSortOrder = _mainSpriteRenderer.sortingOrder;
        }

        public void AddSprite(Sprite sprite, bool preserveOriginalSize = false)
        {
            var additionalRenderer = Instantiate(_additionalRenderer, transform);
            additionalRenderer.SetSprite(sprite, Size, ++_currentSortOrder, preserveOriginalSize);
            _additionalRenderers.Push(additionalRenderer);
        }

        public void SetSize(Vector2 newSize)
        {
            _mainSpriteRenderer.size = newSize;
            
            foreach (var additionalRenderer in _additionalRenderers)
            {
                additionalRenderer.SetSize(newSize);
            }
        }
        
        public void RemoveAdditionalSprite()
        {
            var additionalRenderer = _additionalRenderers.Pop();
            --_currentSortOrder;
            Destroy(additionalRenderer.gameObject);
        }

        public void Reset()
        {
            while (_additionalRenderers.Count != 0)
            {
                RemoveAdditionalSprite();
            }
            
            _mainSpriteRenderer.sprite = null;
            _currentSortOrder = 0;
        }
    }
}