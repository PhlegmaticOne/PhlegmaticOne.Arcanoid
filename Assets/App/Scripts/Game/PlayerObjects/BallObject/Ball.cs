using Game.Behaviors;
using Game.PlayerObjects.Base;
using UnityEngine;

namespace Game.PlayerObjects.BallObject
{
    public class Ball : BehaviorObject<Ball>, IStartMovable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        private float _startSpeed;
        private Vector2 _direction = Vector2.up;
        private float _initialSpeed;

        private void Start() => ToStatic();

        public void Initialize(float initialSpeed)
        {
            _initialSpeed = initialSpeed;
            _startSpeed = initialSpeed;
        }

        public float GetInitialSpeed() => _initialSpeed;
        public void StartMove()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            SetSpeed(_direction * _startSpeed);
        }

        public Transform GetTransform() => transform;
        public Bounds GetBounds() => _boxCollider2D.bounds;
        public Vector2 GetSpeed() => _rigidbody2D.velocity;
        public float GetStartSpeed() => _startSpeed;
        public void SetStartSpeed(float startSpeed) => _startSpeed = startSpeed;
        public void SetDirection(Vector2 direction) => _direction = direction;
        public void SetSpeed(Vector2 speed)
        {
            if (_rigidbody2D.bodyType != RigidbodyType2D.Static)
            {
                _rigidbody2D.velocity = speed;
            }
        }

        protected override void ResetProtected()
        {
            _startSpeed = 0;
            SetSpeed(Vector2.zero);
            ToStatic();
        }

        protected override bool CanBeDestroyedOnDestroyCollision() => true;

        private void ToStatic() => _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
}