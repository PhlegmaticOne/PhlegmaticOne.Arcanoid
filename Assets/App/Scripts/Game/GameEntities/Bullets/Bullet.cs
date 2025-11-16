using Game.GameEntities.Base;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bullets
{
    public class Bullet : BehaviorObject<Bullet>, IDamageable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _damage;
        [SerializeField] private float _startSpeed;

        public float Damage => _damage;
        
        public float StartSpeed => _startSpeed;

        public void StartMove() => _rigidbody.linearVelocity = Vector2.up * _startSpeed;
        protected override bool CanBeDestroyedOnDestroyCollision() => true;
        protected override void ResetProtected() => _rigidbody.linearVelocity = Vector2.zero;
    }
}