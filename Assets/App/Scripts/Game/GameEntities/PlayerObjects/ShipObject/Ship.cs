using Game.GameEntities.PlayerObjects.Base;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject
{
    public class Ship : MonoBehaviour, IDimensionable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PolygonCollider2D _collider2D;

        public Transform GetTransform() => transform;
        public Collider2D GetCollider() => _collider2D;

        public void Enable() => gameObject.SetActive(true);
    }
}