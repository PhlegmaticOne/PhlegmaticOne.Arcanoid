namespace Libs.Pooling.Base
{
    public interface IPoolProvider
    {
        IObjectPool<T> GetPool<T>() where T : IPoolable;
        IObjectPool<T> GetPoolByItemType<T>(T item) where T : IPoolable;
        IAbstractObjectPool<TBase> GetAbstractPool<TBase>() where TBase : IPoolable;
    }
}