using System.Collections.Generic;
using Game.ObjectParticles.Particles;
using Libs.Pooling.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Game.ObjectParticles
{
    public class ParticleManager
    {
        private readonly IPoolProvider _poolProvider;
        private readonly Transform _spawnTransform;

        private readonly List<ParticleBase> _particles;

        public ParticleManager(IPoolProvider poolProvider, Transform spawnTransform)
        {
            _poolProvider = poolProvider;
            _spawnTransform = spawnTransform;
            _particles = new List<ParticleBase>();
        }

        public T SpawnParticle<T>(UnityAction<T> initAction = null) where T : ParticleBase
        {
            var particle = _poolProvider.GetPool<T>().Get();
            particle.transform.SetParent(_spawnTransform);
            initAction?.Invoke(particle);
            particle.OnEnd += RemoveParticle;
            _particles.Add(particle);
            return particle;
        }

        public void Disable()
        {
            for (var i = _particles.Count - 1; i >= 0; i--)
            {
                RemoveParticle(_particles[i]);
            }
        }

        private void RemoveParticle(ParticleBase particleBase)
        {
            var pool = _poolProvider.GetPoolByItemType(particleBase);
            pool.ReturnToPool(particleBase);
            particleBase.OnEnd -= RemoveParticle;
            _particles.Remove(particleBase);
        }
    }
}