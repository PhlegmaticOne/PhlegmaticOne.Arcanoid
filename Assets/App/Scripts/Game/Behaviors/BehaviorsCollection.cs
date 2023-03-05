using System.Collections.Generic;

namespace Game.Behaviors
{
    public class BehaviorsCollection<T> where T : BehaviorObject<T>
    {
        private readonly Dictionary<string, List<IObjectBehavior<T>>> _behaviours;

        public BehaviorsCollection()
        {
            _behaviours = new Dictionary<string, List<IObjectBehavior<T>>>();
        }

        public void AddBehaviour(string colliderTag, IObjectBehavior<T> behaviour)
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviours))
            {
                behaviours.Add(behaviour);
            }
            else
            {
                _behaviours.Add(colliderTag, new List<IObjectBehavior<T>> { behaviour });
            }
        }

        public void RemoveBehaviour(string colliderTag, IObjectBehavior<T> behaviour)
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviours))
            {
                behaviours.Remove(behaviour);
            }
        }

        public void Clear() => _behaviours.Clear();

        public List<IObjectBehavior<T>> GetBehaviours(string colliderKey)
        {
            return _behaviours.TryGetValue(colliderKey, out var behaviours) ? behaviours : new List<IObjectBehavior<T>>();
        }
    }
}