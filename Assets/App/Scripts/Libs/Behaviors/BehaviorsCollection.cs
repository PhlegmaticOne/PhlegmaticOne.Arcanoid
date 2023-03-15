using System.Collections.Generic;
using System.Linq;

namespace Libs.Behaviors
{
    public class BehaviorsCollection<T> where T : BehaviorObject<T>
    {
        private readonly Dictionary<string, List<IObjectBehavior<T>>> _behaviours;

        public BehaviorsCollection() => _behaviours = new Dictionary<string, List<IObjectBehavior<T>>>();
        
        public List<IObjectBehavior<T>> GetAllBehaviors(string colliderKey)
        {
            return _behaviours.TryGetValue(colliderKey, out var behaviours) ? behaviours : new List<IObjectBehavior<T>>();
        }

        public void AddBehavior(string colliderTag, IObjectBehavior<T> behaviour)
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

        public void RemoveBehavior(string colliderTag, IObjectBehavior<T> behaviour)
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviours))
            {
                behaviours.Remove(behaviour);
            }
        }
        
        public TBehavior GetBehavior<TBehavior>(string colliderTag)
            where TBehavior : IObjectBehavior
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviors))
            {
                return (TBehavior)behaviors.SingleOrDefault(x => x is TBehavior);
            }

            return default;
        }

        public TBehavior RemoveBehavior<TBehavior>(string colliderTag) where TBehavior : IObjectBehavior
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviors))
            {
                var behavior = behaviors.SingleOrDefault(x => x is TBehavior);

                if (behaviors.Remove(behavior) == false)
                {
                    return default;
                }

                return (TBehavior)behavior;
            }

            return default;
        }
        
        public TBehavior SubstituteBehavior<TBehavior>(string colliderTag, IObjectBehavior<T> substituteWith)
            where TBehavior : IObjectBehavior<T>
        {
            if (_behaviours.TryGetValue(colliderTag, out var behaviors))
            {
                var behavior = GetBehavior<TBehavior>(colliderTag);
                
                if (behavior == null)
                {
                    return default;
                }
                
                var index = behaviors.IndexOf(behavior);
                behaviors[index] = substituteWith;
                return behavior;
            }

            return default;
        }

        public void ClearBehaviors(string colliderTag)
        {
            if (_behaviours.ContainsKey(colliderTag))
            {
                _behaviours[colliderTag].Clear();
            }
        }
        
        public void ClearAll() => _behaviours.Clear();
    }
}