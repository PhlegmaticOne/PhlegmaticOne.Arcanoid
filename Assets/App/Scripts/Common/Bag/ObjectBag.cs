using System;
using System.Collections.Generic;

namespace Common.Bag
{
    public class ObjectBag : IObjectBag
    {
        private readonly Dictionary<Type, object> _bag = new Dictionary<Type, object>();
        public T Get<T>()
        {
            if (_bag.TryGetValue(typeof(T), out var value))
            {
                return (T)value;
            }
            return default;
        }

        public void Set<T>(T item)
        {
            var type = typeof(T);
            if (_bag.ContainsKey(type))
            {
                _bag[type] = item;
            }
            else
            {
                _bag.Add(typeof(T), item);
            }
        }

        public bool Remove<T>() => _bag.Remove(typeof(T));
    }
}