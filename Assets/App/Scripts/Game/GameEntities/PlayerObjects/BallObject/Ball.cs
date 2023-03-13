using Game.GameEntities.PlayerObjects.Base;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject
{
    public class Ball : BehaviorObject<Ball>, IStartMovable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        //[SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private CircleCollider2D _circleCollider2D;

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
        public Bounds GetBounds() => _circleCollider2D.bounds;
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

        public void AddDeltaSpeed(float deltaSpeed)
        {
            var ballSpeed = GetSpeed();
            var ballSpeedMagnitude = ballSpeed.magnitude;
            var newSpeed = (deltaSpeed + ballSpeedMagnitude) * ballSpeed.normalized;
            SetSpeed(newSpeed);
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