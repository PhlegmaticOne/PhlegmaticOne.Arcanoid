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

        public void Reset() => _spriteRenderer.sprite = null;
    }
}