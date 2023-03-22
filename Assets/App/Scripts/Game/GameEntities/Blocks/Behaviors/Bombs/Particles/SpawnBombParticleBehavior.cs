using Game.ObjectParticles;
using Game.ObjectParticles.Particles;
using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Particles
{
    public class SpawnBombParticleBehavior : IObjectBehavior<Block>
    {
        private readonly ParticleManager _particleManager;

        public SpawnBombParticleBehavior(ParticleManager particleManager)
        {
            _particleManager = particleManager;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var particle = _particleManager.SpawnParticle<BombParticle>(p =>
            {
                p.transform.position = entity.transform.position;
            });
            particle.Play();
        }
    }
}