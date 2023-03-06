using System;

namespace Libs.Pooling.Base
{
    public interface IObjectPool : IDisposable
    {
        void ReturnToPool(IPoolable item);
    }

    public interface IObjectPool<out T> : IObjectPool where T : IPoolable
    {
        T Get();
    }
}


