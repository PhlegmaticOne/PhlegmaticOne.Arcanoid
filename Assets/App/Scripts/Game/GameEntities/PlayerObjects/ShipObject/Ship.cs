using System;
using Game.GameEntities.PlayerObjects.Base;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject
{
    public class Ship : MonoBehaviour, IControlable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PolygonCollider2D _collider2D;
        [SerializeField] private ShipConfiguration _shipConfiguration;
        [SerializeField] private ActiveShipPart _activeShipPart;

        private float _currentControlLerp;

        private void Start() => _currentControlLerp = _shipConfiguration.StartControlLerp;
        public void IncreaseLerpBy(float multiplier) => _currentControlLerp *= multiplier;
        public void DecreaseLerpBy(float divider) => _currentControlLerp /= divider;

        public void IncreaseScaleBy(float multiplier, float time) => _activeShipPart.ScaleBy(multiplier, time);

        public void DecreaseScaleBy(float divider, float time)
        {
            var multiplier = 1 / divider;
            _activeShipPart.ScaleBy(multiplier, time);
        }

        public Transform GetTransform() => transform;
        public Collider2D GetCollider() => _collider2D;

        public void Enable() => gameObject.SetActive(true);
        public float ControlLerp => _currentControlLerp;
    }
}