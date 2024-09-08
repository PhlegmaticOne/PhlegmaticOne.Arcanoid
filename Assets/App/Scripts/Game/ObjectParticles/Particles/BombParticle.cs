using UnityEngine;

namespace Game.ObjectParticles.Particles
{
    public class BombParticle : ParticleBase
    {
        [SerializeField] private ParticleSystem _otherParticle;
        public override void Play()
        {
            base.Play();
            _otherParticle.Play();
        }

        public override void Reset()
        {
            StopParticle(_otherParticle);
            base.Reset();
        }
    }
}