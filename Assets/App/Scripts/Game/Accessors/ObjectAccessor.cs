namespace Game.Accessors
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        private T _value;
        
        public T Get() => _value;

        public void Set(T value) => _value = value;
    }
}