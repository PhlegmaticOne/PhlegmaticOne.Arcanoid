namespace Abstracts.Pooling.Base
{
    public interface IPoolableBehaviour<in T> where T : IPoolable
    {
        void Enable(T item);
        void Disable(T item);
        void Destroy(T item);
    }
}