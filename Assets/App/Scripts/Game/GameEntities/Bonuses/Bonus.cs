using Game.GameEntities.Bonuses.Configurations;
using Game.GameEntities.Bonuses.View;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bonuses
{
    public class Bonus : BehaviorObject<Bonus>
    {
        [SerializeField] private BonusView _bonusView;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private BoxCollider2D _boxCollider;

        private Vector2 _awakeSize;

        private Vector2 _velocity;
        public BonusConfiguration BonusConfiguration { get; private set; }

        public void Initialize(BonusConfiguration bonusConfiguration)
        {
            _bonusView.Initialize(bonusConfiguration);
            BonusConfiguration = bonusConfiguration;
            _velocity = new Vector2(0, bonusConfiguration.StartDownSpeed);
        }

        private void Awake() => _awakeSize = _boxCollider.size;

        public void MultiplySize(float multiplier)
        {
            _bonusView.MultiplySize(multiplier);
            _boxCollider.size *= multiplier;
        }

        public void StartMove()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.velocity = _velocity;
        }

        protected override bool CanBeDestroyedOnDestroyCollision() => true;
        protected override void ResetProtected()
        {
            _boxCollider.size = _awakeSize;
            _bonusView.SetSize(_awakeSize);
            _bonusView.Reset();
            BonusConfiguration = null;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}