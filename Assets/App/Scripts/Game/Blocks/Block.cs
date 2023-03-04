using System;
using System.Collections.Generic;
using Game.Blocks.CollisionBehaviours.Base;
using Game.Blocks.Configurations;
using Game.Blocks.View;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.Blocks
{
    public class Block : MonoBehaviour, IPoolable
    {
        private readonly List<ICollisionBehaviour> _collisionBehaviours = new List<ICollisionBehaviour>();
        
        [SerializeField] private BlockView _blockView;
        [SerializeField] private BoxCollider2D _boxCollider;
        
        public BlockConfiguration BlockConfiguration { get; private set; }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetSize(Vector2 size)
        {
            _blockView.SetSize(size);
            _boxCollider.size = _blockView.Size;
        }

        public void Initialize(BlockConfiguration configuration)
        {
            BlockConfiguration = configuration;
            _blockView.SetSprite(configuration.BlockSprite);
        }

        public float GetBaseHeight() => _boxCollider.size.y;

        public void AddBehaviour(ICollisionBehaviour collisionBehaviour) => 
            _collisionBehaviours.Add(collisionBehaviour);

        public void RemoveBehaviour(ICollisionBehaviour collisionBehaviour) => 
            _collisionBehaviours.Remove(collisionBehaviour);

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(BlockConfiguration.CollidesWithTag))
            {
                return;
            }
            
            foreach (var collisionBehaviour in _collisionBehaviours)
            {
                collisionBehaviour.OnCollision(this);
            }
        }

        private void OnMouseDown()
        {
            foreach (var collisionBehaviour in _collisionBehaviours)
            {
                collisionBehaviour.OnCollision(this);
            }
        }

        public void Reset()
        {
            
        }
    }
}