using UnityEngine;

namespace Game.ObjectParticles.Particles
{
    public class BlockParticle : ParticleBase
    {
        public void SetColor(Color color)
        {
            var main = ParticleSystem.main;
            main.startColor = color;
        }
    }
}