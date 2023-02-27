using System;
using System.Collections.Generic;
using Abstracts.Pooling.Base;

namespace Abstracts.Pooling.Implementation
{
    public class PoolProvider : IPoolProvider
    {
        private readonly Dictionary<Type, IObjectPool> _poolsAvailable = new Dictionary<Type, IObjectPool>();

        public PoolProvider AddPool<T>(IObjectPool<T> pool) where T : IPoolable
        {
            _poolsAvailable.Add(typeof(T), pool);
            return this;
        }

        public IObjectPool<T> GetPool<T>() where T : IPoolable => (IObjectPool<T>)_poolsAvailable[typeof(T)];
    }
}