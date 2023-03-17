using Libs.Pooling.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ObjectParticles.Particles
{
    public class ParticleBase : MonoBehaviour, IPoolable
    {
        [SerializeField] protected ParticleSystem ParticleSystem;

        public void Play() => ParticleSystem.Play();
        
        public event UnityAction<ParticleBase> OnEnd;
        private void OnParticleSystemStopped() => OnEnd?.Invoke(this);

        public void Reset() 
        {
            if (ParticleSystem.isStopped == false)
            {
                ParticleSystem.Stop();
            }
        }
    }
}