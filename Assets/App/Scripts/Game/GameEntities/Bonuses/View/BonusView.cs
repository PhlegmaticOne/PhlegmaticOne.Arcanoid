using Game.GameEntities.Bonuses.Configurations;
using UnityEngine;

namespace Game.GameEntities.Bonuses.View
{
    public class BonusView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Initialize(BonusConfiguration bonusConfiguration)
        {
            _spriteRenderer.sprite = bonusConfiguration.BonusSprite;
        }

        public void MultiplySize(float multiplier)
        {
            var size = _spriteRenderer.size * multiplier;
            SetSize(size);
        }

        public void SetSize(Vector2 size)
        {
            _spriteRenderer.size = size;
        }

        public void Reset() => _spriteRenderer.sprite = null;
    }
}