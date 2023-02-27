using System.Collections.Generic;
using Abstracts.Pooling.Base;

namespace Abstracts.Pooling.Implementation
{
    public class ObjectPool<T> : IObjectPool<T> where T : IPoolable
    {
        private readonly Stack<T> _items;
        private readonly ICreationStrategy<T> _creationStrategy;
        private readonly IPoolableBehaviour<T> _poolableBehaviour;
        private readonly int _initialCapacity;
        private readonly int _maxCapacity;
        private readonly bool _destroyItemsOnOverflow;

        public ObjectPool(ICreationStrategy<T> creationStrategy,
            IPoolableBehaviour<T> poolableBehaviour, 
            int initialCapacity,
            int maxCapacity,
            bool destroyItemsOnOverflow = true)
        {
            _items = new Stack<T>(initialCapacity);
            _creationStrategy = creationStrategy;
            _poolableBehaviour = poolableBehaviour;
            _initialCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _destroyItemsOnOverflow = destroyItemsOnOverflow;
            Initialize();
        }

        public T Get()
        {
            var item = _items.Count != 0 ? _items.Pop() : _creationStrategy.Create();
            _poolableBehaviour.Enable(item);
            return item;
        }

        public void ReturnToPool(IPoolable item)
        {
            if (!(item is T generic))
            {
                return;
            }

            if (_items.Count == _maxCapacity && _destroyItemsOnOverflow)
            {
                _poolableBehaviour.Destroy(generic);
                return;
            }
            
            generic.Reset();
            _poolableBehaviour.Disable(generic);
            _items.Push(generic);
        }
        
        
        public void Dispose()
        {
            foreach (var item in _items)
            {
                _poolableBehaviour.Destroy(item);
            }
            _items.Clear();
        }
        
        private void Initialize()
        {
            for (var i = 0; i < _initialCapacity; i++)
            {
                var item = _creationStrategy.Create();
                _poolableBehaviour.Disable(item);
                _items.Push(item);
            }
        }
    }
}