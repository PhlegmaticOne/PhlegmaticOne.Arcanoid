using System;
using Game.PlayerObjects.Base;
using UnityEngine;

namespace Game.PlayerObjects.ShipObject
{
    public class Ship : MonoBehaviour, IDimensionable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        public Transform GetTransform() => transform;
        public Bounds GetBounds() => _boxCollider2D.bounds;
        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}