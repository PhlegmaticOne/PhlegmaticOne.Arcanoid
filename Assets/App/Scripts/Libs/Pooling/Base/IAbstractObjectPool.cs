using System;

namespace Libs.Pooling.Base
{
    public interface IAbstractObjectPool<T> : IObjectPool where T : IPoolable
    {
        TImpl GetConcrete<TImpl>() where TImpl : T;
        T GetByType(Type type);
    }
}