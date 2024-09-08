using System;
using Game.GameEntities.Base;
using Game.GameEntities.PlayerObjects.Base;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject
{
    public class Ball : BehaviorObject<Ball>, IStartMovable, IDamageable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private CircleCollider2D _circleCollider2D;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _rageColor;
        [SerializeField] private float _damage = 1f;
        private bool _isRage;
        private Vector2 _preCollisionSpeed;

        private float _startSpeed;
        private float _currentSpeed;
        public float Damage => _damage;
        public bool IsRage => _isRage;
        
        public void Initialize(float initialSpeed)
        {
            _startSpeed = initialSpeed;
            _currentSpeed = initialSpeed;
        }

        public Vector2 GetPreCollisionSpeed() => _preCollisionSpeed;

        public Collider2D GetCollider() => _circleCollider2D;

        public void StartMove(Vector2 direction)
        {
            if (_isRage)
            {
                _trailRenderer.gameObject.SetActive(true);
            }
            SetSpeed(direction * _startSpeed);
        }

        public Transform GetTransform() => transform;
        public Vector2 GetSpeed() => _rigidbody2D.velocity;
        public float CurrentSpeed => _currentSpeed;
        public void SetStartSpeed(float startSpeed)
        {
            _startSpeed = startSpeed;
            _currentSpeed = startSpeed;
        }

        public void SetSpeed(Vector2 speed)
        {
            if (_rigidbody2D.bodyType != RigidbodyType2D.Static)
            {
                _rigidbody2D.velocity = speed;
            }
        }

        private void LateUpdate()
        {
            _rigidbody2D.velocity = _currentSpeed * _rigidbody2D.velocity.normalized;
        }

        private void FixedUpdate()
        {
            _preCollisionSpeed = _rigidbody2D.velocity;
        }

        public void AddDeltaSpeed(float deltaSpeed)
        {
            var ballSpeed = GetSpeed();
            var ballSpeedMagnitude = ballSpeed.magnitude;
            var newSpeed = (deltaSpeed + ballSpeedMagnitude) * ballSpeed.normalized;
            SetSpeed(newSpeed);
            _currentSpeed += deltaSpeed;
        }

        public void DisableTrail() => _trailRenderer.gameObject.SetActive(false);

        public void CopyToBall(Ball ball)
        {
            InstallOnCollisionBehaviorsTo(ball);
            ball.ChangeRageMode(_isRage);
        }

        public void ChangeRageMode(bool isRage)
        {
            _isRage = isRage;
            _trailRenderer.gameObject.SetActive(isRage);
            var color = isRage ? _rageColor : Color.white;
            _spriteRenderer.color = color;
        }

        protected override void ResetProtected()
        {
            _startSpeed = 0;
            _currentSpeed = 0;
            SetSpeed(Vector2.zero);

            if (_isRage)
            {
                ChangeRageMode(false);
            }
        }

        protected override bool CanBeDestroyedOnDestroyCollision() => true;
    }
}