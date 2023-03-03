using System;
using System.Collections.Generic;
using Libs.Pooling.Base;

namespace Libs.Pooling.Implementation
{
    public class PoolBuilder
    {
        private readonly Dictionary<Type, IObjectPool> _poolsAvailable;
        private readonly Dictionary<Type, IObjectPool> _abstractPoolsAvailable;

        private PoolBuilder()
        {
            _poolsAvailable = new Dictionary<Type, IObjectPool>();
            _abstractPoolsAvailable = new Dictionary<Type, IObjectPool>();
        }
        
        public static PoolBuilder Create() => new PoolBuilder();
        
        public PoolBuilder AddPool<T>(IObjectPool<T> pool)
            where T : IPoolable
        {
            _poolsAvailable.Add(typeof(T), pool);
            return this;
        }

        public PoolBuilder AddAbstractPool<TBase>(IAbstractObjectPool<TBase> abstractObjectPool)
            where TBase : IPoolable
        {
            _abstractPoolsAvailable.Add(typeof(TBase), abstractObjectPool);
            return this;
        }

        public IPoolProvider BuildProvider() => new PoolProvider(_poolsAvailable, _abstractPoolsAvailable);
    }
}