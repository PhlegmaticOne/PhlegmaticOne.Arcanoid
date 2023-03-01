using System;

namespace Abstracts.Pooling.Base
{
    public interface IObjectPool : IDisposable
    {
        void ReturnToPool(IPoolable item);
    }

    public interface IObjectPool<out T> : IObjectPool where T : IPoolable
    {
        T Get();
    }

    public interface IAbstractObjectPool<in T> : IObjectPool where T : IPoolable
    {
        TImpl GetConcrete<TImpl>() where TImpl : T;
    }
}


