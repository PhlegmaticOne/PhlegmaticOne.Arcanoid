using System.Collections.Generic;
using System.Linq;
using Libs.Pooling.Base;
using UnityEngine;

namespace Libs.Behaviors
{
    public abstract class BehaviorObject<TSelf> : MonoBehaviour, IPoolable
        where TSelf : BehaviorObject<TSelf>
    {
        [SerializeField] private BehaviorObjectTags _behaviorObjectTags;
        private bool _markedToDestroy;
        
        private readonly BehaviorsCollection<TSelf> _onCollisionBehaviours = new BehaviorsCollection<TSelf>();
        private readonly BehaviorsCollection<TSelf> _onDestroyBehaviours = new BehaviorsCollection<TSelf>();
        
        public BehaviorsCollection<TSelf> OnCollisionBehaviors => _onCollisionBehaviours;
        public BehaviorsCollection<TSelf> OnDestroyBehaviors => _onDestroyBehaviours;
        public BehaviorObjectTags BehaviorObjectTags => _behaviorObjectTags;

        public void InstallOnCollisionBehaviorsTo(BehaviorObject<TSelf> newObject)
        {
            var behaviors = newObject.OnCollisionBehaviors;
            behaviors.ClearAll();

            foreach (var onCollisionBehaviour in _onCollisionBehaviours)
            {
                foreach (var behavior in onCollisionBehaviour.Value)
                {
                    behaviors.AddBehavior(onCollisionBehaviour.Key, behavior);
                }
            }
        }

        public void MarkToDestroy() => _markedToDestroy = true;
        
        protected abstract bool CanBeDestroyedOnDestroyCollision();

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<BehaviorObjectTags>(out var behaviorObjectTags) == false)
            {
                return;
            }

            foreach (var colliderTag in behaviorObjectTags.ColliderTags)
            {
                var collisionBehaviours = _onCollisionBehaviours.GetAllBehaviors(colliderTag.Tag);
                var destroyBehaviours = _onDestroyBehaviours.GetAllBehaviors(colliderTag.Tag);
            
                if ((CanBeDestroyedOnDestroyCollision() || _markedToDestroy) && destroyBehaviours.Count != 0)
                {
                    ExecuteBehaviours(destroyBehaviours, col);
                    return;
                }

                if (collisionBehaviours.Count != 0)
                {
                    ExecuteBehaviours(collisionBehaviours, col);
                }
            }
        }

        public void DestroyWithTag(string colliderTag, Collision2D originalCollision)
        {
            ExecuteBehaviours(_onDestroyBehaviours.GetAllBehaviors(colliderTag), originalCollision);
        }
        
        public void CollideWithTag(string colliderTag, Collision2D originalCollision)
        {
            ExecuteBehaviours(_onCollisionBehaviours.GetAllBehaviors(colliderTag), originalCollision);
        }

        public void Reset()
        {
            _onCollisionBehaviours.ClearAll();
            _onDestroyBehaviours.ClearAll();
            ResetProtected();
        }
        
        protected virtual void ResetProtected() { }

        private void ExecuteBehaviours(List<IObjectBehavior<TSelf>> behaviours, Collision2D collision2D)
        {
            foreach (var behaviour in behaviours.ToList())
            {
                behaviour.Behave(Self(), collision2D);
            }
        }

        private TSelf Self() => (TSelf)this;
    }
}