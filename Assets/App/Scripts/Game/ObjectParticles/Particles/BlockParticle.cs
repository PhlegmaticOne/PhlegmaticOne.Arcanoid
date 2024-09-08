using UnityEngine;

namespace Game.ObjectParticles.Particles
{
    public class BlockParticle : ParticleBase
    {
        public void SetColor(Color color)
        {
            var main = MainParticleSystem.main;
            main.startColor = color;
        }
    }
}