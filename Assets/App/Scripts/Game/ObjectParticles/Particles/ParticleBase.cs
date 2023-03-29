using Libs.Pooling.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ObjectParticles.Particles
{
    public abstract class ParticleBase : MonoBehaviour, IPoolable
    {
        [SerializeField] protected ParticleSystem MainParticleSystem;
        public ParticleSystem Main => MainParticleSystem;

        public virtual void Play() => MainParticleSystem.Play();
        
        public event UnityAction<ParticleBase> OnEnd;
        private void OnParticleSystemStopped() => OnEnd?.Invoke(this);

        public virtual void Reset() 
        {
            StopParticle(MainParticleSystem);
        }

        protected void StopParticle(ParticleSystem particle)
        {
            if (particle.isStopped == false)
            {
                particle.Stop();
            }
        }
    }
}