using System.Collections.Generic;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Behaviors
{
    public abstract class BehaviorObject<TSelf> : MonoBehaviour, IPoolable
        where TSelf : BehaviorObject<TSelf>
    {
        private bool _markedToDestroy;
        
        private readonly BehaviorsCollection<TSelf> _onCollisionBehaviours = new BehaviorsCollection<TSelf>();
        private readonly BehaviorsCollection<TSelf> _onDestroyBehaviours = new BehaviorsCollection<TSelf>();

        public void AddOnCollisionBehaviour(string colliderTag, IObjectBehavior<TSelf> behaviour) => 
            _onCollisionBehaviours.AddBehaviour(colliderTag, behaviour);

        public void AddOnDestroyBehaviour(string colliderTag, IObjectBehavior<TSelf> behaviour) => 
            _onDestroyBehaviours.AddBehaviour(colliderTag, behaviour);

        public void RemoveOnCollisionBehaviour(string colliderTag, IObjectBehavior<TSelf> behaviour) => 
            _onCollisionBehaviours.RemoveBehaviour(colliderTag, behaviour);

        public void RemoveOnDestroyBehaviour(string colliderTag, IObjectBehavior<TSelf> behaviour) => 
            _onCollisionBehaviours.AddBehaviour(colliderTag, behaviour);

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
                var collisionBehaviours = _onCollisionBehaviours.GetBehaviours(colliderTag.Tag);
                var destroyBehaviours = _onDestroyBehaviours.GetBehaviours(colliderTag.Tag);
            
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

        public void DestroyWithTag(string colliderTag)
        {
            ExecuteBehaviours(_onDestroyBehaviours.GetBehaviours(colliderTag), null);
        }
        
        public void CollideWithTag(string colliderTag)
        {
            ExecuteBehaviours(_onCollisionBehaviours.GetBehaviours(colliderTag), null);
        }

        public void Reset()
        {
            _onCollisionBehaviours.Clear();
            _onDestroyBehaviours.Clear();
            ResetProtected();
        }
        
        protected virtual void ResetProtected() { }

        private void ExecuteBehaviours(List<IObjectBehavior<TSelf>> behaviours, Collision2D collision2D)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Behave(Self(), collision2D);
            }
        }

        private TSelf Self() => (TSelf)this;
    }
}