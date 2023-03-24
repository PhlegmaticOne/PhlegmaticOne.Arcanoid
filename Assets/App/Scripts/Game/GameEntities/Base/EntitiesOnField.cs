using System.Collections.Generic;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Base
{
    public class EntitiesOnField<T> : MonoBehaviour where T : BehaviorObject<T>
    {
        protected readonly List<T> _all = new List<T>();
        public IReadOnlyList<T> All => _all;

        public void Add(T behaviorObject)
        {
            _all.Add(behaviorObject);
            OnAdded(behaviorObject);
        }

        public void Remove(T behaviorObject)
        {
            _all.Remove(behaviorObject);
            OnRemoved(behaviorObject);
        }
        
        protected virtual void OnAdded(T behaviorObject) { }
        protected virtual void OnRemoved(T behaviorObject) { }

        public void Clear() => _all.Clear();
    }
}