using Game.Behaviors;
using UnityEngine;

namespace Game.PlayerObjects.BallObject
{
    public class Ball : BehaviorObject<Ball>
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private float _startSpeed;

        public void Initialize(float startSpeed)
        {
            _startSpeed = startSpeed;
        }

        public void StartMove()
        {
            SetSpeed(Vector2.down * _startSpeed);
        }

        public Vector2 GetSpeed() => _rigidbody2D.velocity;
        public float GetStartSpeed() => _startSpeed;
        public void SetSpeed(Vector2 speed) => _rigidbody2D.velocity = speed;
        
        protected override bool CanBeDestroyedOnDestroyCollision() => true;
    }
}