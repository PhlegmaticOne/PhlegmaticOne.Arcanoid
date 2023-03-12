namespace Common.Bag
{
    public interface IObjectBag
    {
        T Get<T>();
        void Set<T>(T item);
        bool Remove<T>();
    }
}