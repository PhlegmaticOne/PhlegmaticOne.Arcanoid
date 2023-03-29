using System.Linq;
using Game.ObjectParticles;
using Game.ObjectParticles.Particles;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Particles
{
    public class SpawnSideWallParticleBehavior : IObjectBehavior<Ball>
    {
        private readonly ParticleManager _particleManager;
        private readonly ColliderTag _bottomColliderTag;

        private Color _bottomColor;
        private Color _rageColor;
        private float _bottomSize;
        
        public bool IsDefault => false;

        public SpawnSideWallParticleBehavior(ParticleManager particleManager,
            ColliderTag bottomColliderTag)
        {
            _particleManager = particleManager;
            _bottomColliderTag = bottomColliderTag;
        }

        public void SetBehaviorParameters(Color rageColor, Color bottomColor, float bottomSize)
        {
            _bottomColor = bottomColor;
            _bottomSize = bottomSize;
            _rageColor = rageColor;
        }
        
        public void Behave(Ball entity, Collision2D collision2D)
        {
            var particle = _particleManager.SpawnParticle<WallParticle>(x =>
            {
                InitializeParticle(entity, x.Main, collision2D);
            });
            particle.Play();
        }

        private void InitializeParticle(Ball ball, ParticleSystem particleSystem, Collision2D collision2D)
        {
            var contact = collision2D.contacts[0];
            var angle = AngleBetweenVectors(Vector2.right, contact.normal);
            var shape = particleSystem.shape;
            var main = particleSystem.main;
            
            var shapeRotation = shape.rotation;
            shapeRotation.z += angle;
            shape.position = contact.point;
            shape.rotation = shapeRotation;

            if (ball.IsRage)
            {
                main.startColor = _rageColor;
            }

            if (collision2D.collider.TryGetComponent<BehaviorObjectTags>(out var tags) &&
                tags.ColliderTags.Contains(_bottomColliderTag))
            {
                main.startColor = _bottomColor;
                main.startSize = new ParticleSystem.MinMaxCurve(_bottomSize);
            }
        }
        
        private static float AngleBetweenVectors(Vector2 vec1, Vector2 vec2)
        {
            var vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
            var sign = Vector2.Dot(vec1Rotated90, vec2) < 0 ? -1.0f : 1.0f;
            return Vector2.Angle(vec1, vec2) * sign;
        }
    }
}