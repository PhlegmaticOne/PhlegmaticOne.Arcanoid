using UnityEngine;

namespace Game.GameEntities.Bonuses.Configurations
{
    [CreateAssetMenu(menuName = "Game/Bonuses/Create bonus configuration", order = 0)]
    public class BonusConfiguration : ScriptableObject
    {
        [SerializeField] private int _bonusId;
        [SerializeField] private Sprite _bonusSprite;
        [SerializeField] private float _startDownSpeed;

        public int BonusId => _bonusId;
        public Sprite BonusSprite => _bonusSprite;
        public float StartDownSpeed => _startDownSpeed;
    }
}