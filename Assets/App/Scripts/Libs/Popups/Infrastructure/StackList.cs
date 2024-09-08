using System.Collections.Generic;
using System.Linq;

namespace Libs.Popups.Infrastructure
{
    public class StackList<T>
    {
        private readonly List<T> _items;

        public StackList() => _items = new List<T>();
        public int Count => _items.Count;
        public T Peek() => _items[_items.Count - 1];

        public T Pop()
        {
            var item = Peek();
            _items.Remove(item);
            return item;
        }

        public void Push(T item) => _items.Add(item);
        public void Remove(T item) => _items.Remove(item);
        public List<T> ToList() => _items.ToList();
    }
}