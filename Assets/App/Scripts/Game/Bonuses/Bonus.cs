using Game.Behaviors;
using Game.Bonuses.Configurations;
using Game.Bonuses.View;
using TMPro;
using UnityEngine;

namespace Game.Bonuses
{
    public class Bonus : BehaviorObject<Bonus>
    {
        [SerializeField] private BonusView _bonusView;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _velocity;
        public BonusConfiguration BonusConfiguration { get; private set; }

        public void Initialize(BonusConfiguration bonusConfiguration)
        {
            _bonusView.Initialize(bonusConfiguration);
            BonusConfiguration = bonusConfiguration;
            _velocity = new Vector2(0, bonusConfiguration.StartDownSpeed);
        }

        public void StartMove()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.velocity = _velocity;
        }

        protected override bool CanBeDestroyedOnDestroyCollision() => true;
        protected override void ResetProtected()
        {
            _bonusView.Reset();
            BonusConfiguration = null;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}