namespace Game.Accessors
{
    public interface IObjectAccessor<T>
    {
        T Get();
        void Set(T value);
        void Reset();
    }
}