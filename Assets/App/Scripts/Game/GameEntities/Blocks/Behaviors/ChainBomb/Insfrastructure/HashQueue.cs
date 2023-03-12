using System.Collections.Generic;
using System.Linq;

namespace Game.GameEntities.Blocks.Behaviors.ChainBomb.Insfrastructure
{
    public class HashQueue<T>
    {
        private readonly HashSet<T> _hashSet = new HashSet<T>();
        private readonly Queue<T> _queue = new Queue<T>();
        public void Enqueue(T item)
        {
            if (_hashSet.Add(item))
            {
                _queue.Enqueue(item);
            }
        }

        public T Dequeue() => _queue.Dequeue();
        
        public bool Any() => _queue.Count > 0;

        public List<T> ToList() => _hashSet.ToList();

        public override string ToString() => $"Count: {_queue.Count}";
    }
}