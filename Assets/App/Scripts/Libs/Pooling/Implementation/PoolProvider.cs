using System;
using System.Collections.Generic;
using Libs.Pooling.Base;

namespace Libs.Pooling.Implementation
{
    public class PoolProvider : IPoolProvider
    {
        private readonly Dictionary<Type, IObjectPool> _objectPoolsAvailable;
        private readonly Dictionary<Type, IObjectPool> _abstractPoolsAvailable;

        public PoolProvider(Dictionary<Type, IObjectPool> objectPoolsAvailable, 
            Dictionary<Type, IObjectPool> abstractPoolsAvailable)
        {
            _objectPoolsAvailable = objectPoolsAvailable;
            _abstractPoolsAvailable = abstractPoolsAvailable;
        }

        public IObjectPool<T> GetPool<T>() where T : IPoolable => 
            (IObjectPool<T>)_objectPoolsAvailable[typeof(T)];

        public IObjectPool<T> GetPoolByItemType<T>(T item) where T : IPoolable => 
            (IObjectPool<T>)_objectPoolsAvailable[item.GetType()];
        
        public IAbstractObjectPool<TBase> GetAbstractPool<TBase>() where TBase : IPoolable => 
            (IAbstractObjectPool<TBase>)_abstractPoolsAvailable[typeof(TBase)];
    }
}