using Game.ObjectParticles;
using Game.ObjectParticles.Particles;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Particles
{
    public class SpawnBlockParticleBehavior : IObjectBehavior<Block>
    {
        private readonly ParticleManager _particleManager;
        
        public SpawnBlockParticleBehavior(ParticleManager particleManager) => _particleManager = particleManager;
        
        public bool IsDefault => true;

        public void Behave(Block entity, Collision2D collision2D)
        {
            var particle = _particleManager.SpawnParticle<BlockParticle>(p =>
            {
                p.transform.position = entity.transform.position;
                p.SetColor(entity.BlockConfiguration.UnderlyingBlockConfiguration.Color);
            });
            particle.Play();
        }
    }
}