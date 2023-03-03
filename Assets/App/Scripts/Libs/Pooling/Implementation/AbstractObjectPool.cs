using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Pooling.Base;

namespace Libs.Pooling.Implementation
{
    public class AbstractObjectPool<T> : IAbstractObjectPool<T> where T : IPoolable
    {
        private readonly IPoolableBehaviour<T> _poolableBehaviour;
        private readonly Dictionary<Type, T> _items;
        private readonly List<ICreationStrategy<T>> _creationStrategies;
        private readonly List<T> _created;

        public AbstractObjectPool(IEnumerable<ICreationStrategy<T>> concretes, IPoolableBehaviour<T> poolableBehaviour)
        {
            _creationStrategies = concretes.ToList();
            _poolableBehaviour = poolableBehaviour;
            _items = new Dictionary<Type, T>();
            _created = new List<T>();
        }
        
        private bool Pooled<TImpl>() => _created.Any(x => x is TImpl);
        public TImpl GetConcrete<TImpl>() where TImpl : T
        {
            TImpl result;

            if (Pooled<TImpl>())
            {
                result = CreateItem<TImpl>(false);
            }
            else
            {
                var item = _items.FirstOrDefault(x => x.Key == typeof(TImpl));
                result = item.Value == null ? CreateItem<TImpl>(true) : (TImpl)_items[typeof(TImpl)];
            }

            _poolableBehaviour.Enable(result);
            _created.Add(result);
            return result;
        }
        
        public void ReturnToPool(IPoolable item)
        {
            if (!(item is T generic))
            {
                return;
            }

            generic.Reset();
            _poolableBehaviour.Disable(generic);
            _created.Remove(generic);

            if (Pooled(generic))
            {
                _poolableBehaviour.Destroy(generic);
            }
        }
        
        public void Dispose()
        {
            foreach (var item in _items)
            {
                _poolableBehaviour.Disable(item.Value);
            }
            _items.Clear();
        }

        private bool Pooled(T item) => _created.Any(x => x.GetType() == item.GetType());

        private TItem CreateItem<TItem>(bool addToDictionary) where TItem : T
        {
            var creationStrategy = _creationStrategies.First(x => x.ObjectType == typeof(TItem));
            
            var item = creationStrategy.Create();
            _poolableBehaviour.Disable(item);
            
            if (addToDictionary)
            {
                _items.Add(creationStrategy.ObjectType, item);
            }
            
            return (TItem)item;
        }
    }
}