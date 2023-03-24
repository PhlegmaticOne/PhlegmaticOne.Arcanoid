using System.Collections.Generic;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.View
{
    public class BlockView : MonoBehaviour
    {
        private int _currentSortOrder;
        
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private AdditionalRenderer _additionalRenderer;

        private readonly Stack<AdditionalRenderer> _additionalRenderers = new Stack<AdditionalRenderer>();
        private BlockCracksConfiguration _blockCracksConfiguration;
        public Vector2 Size => _mainSpriteRenderer.size;

        public void Initialize(BlockConfiguration blockConfiguration, BlockCracksConfiguration blockCracksConfiguration)
        {
            SetMainSprite(blockConfiguration.BlockSprite);
            
            foreach (var additionalSprite in blockConfiguration.AdditionalSprites)
            {
                AddSprite(additionalSprite, true);
            }

            _blockCracksConfiguration = blockCracksConfiguration;
        }

        public void Damage(int stage)
        {
            var crackSprites = _blockCracksConfiguration.CrackSprites;
            var spriteIndex = stage % crackSprites.Count;
            
            if (spriteIndex < 0 || spriteIndex >= crackSprites.Count)
            {
                return;
            }
            
            var crackSprite = crackSprites[spriteIndex];
            AddSprite(crackSprite);
        }

        private void SetMainSprite(Sprite sprite)
        {
            _mainSpriteRenderer.sprite = sprite;
            _currentSortOrder = _mainSpriteRenderer.sortingOrder;
        }

        private void AddSprite(Sprite sprite, bool preserveOriginalSize = false)
        {
            var additionalRenderer = Instantiate(_additionalRenderer, transform);
            additionalRenderer.Initialize(sprite, Size, ++_currentSortOrder, preserveOriginalSize);
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
        
        public void Reset()
        {
            while (_additionalRenderers.Count != 0)
            {
                RemoveAdditionalSprite();
            }
            
            _mainSpriteRenderer.sprite = null;
            _currentSortOrder = 0;
        }
        
        private void RemoveAdditionalSprite()
        {
            var additionalRenderer = _additionalRenderers.Pop();
            --_currentSortOrder;
            Destroy(additionalRenderer.gameObject);
        }

    }
}