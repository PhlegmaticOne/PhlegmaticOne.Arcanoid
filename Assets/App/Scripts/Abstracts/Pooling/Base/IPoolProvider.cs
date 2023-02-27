namespace Abstracts.Pooling.Base
{
    public interface IPoolProvider
    {
        IObjectPool<T> GetPool<T>() where T : IPoolable;
    }
}