using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.GameEntities.PlayerObjects.Base;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject
{
    public class Ship : BehaviorObject<Ship>, IControlable
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PolygonCollider2D _collider2D;
        [SerializeField] private ShipConfiguration _shipConfiguration;
        [SerializeField] private ActiveShipPart _activeShipPart;
        [SerializeField] private Transform _unactivePartTransform;
        [SerializeField] private List<Transform> _shotgunTransforms;
        [SerializeField] private BehaviorObjectInstaller<Ship> _shipInstaller;
        private Vector3 _awakePosition;
        
        private float _currentControlLerp;
        public List<Transform> ShotgunTransforms => _shotgunTransforms;
        public Transform ActivePartTransform => _activeShipPart.transform;
        public Transform UnactivePartTransform => _unactivePartTransform;

        private void Awake() => _awakePosition = transform.position;

        private void Start()
        {
            _currentControlLerp = _shipConfiguration.StartControlLerp;
            _shipInstaller.InstallCollisionBehaviours(this);
        }

        public void IncreaseLerpBy(float multiplier) => _currentControlLerp *= multiplier;
        public void DecreaseLerpBy(float divider) => _currentControlLerp /= divider;

        public void IncreaseScaleBy(float multiplier, float time) => _activeShipPart.ScaleBy(multiplier, time);

        public void DecreaseScaleBy(float divider, float time)
        {
            var multiplier = 1 / divider;
            _activeShipPart.ScaleBy(multiplier, time);
        }

        public new void Reset()
        {
            transform.DOKill();
            transform.position = _awakePosition;
        }

        public Transform GetTransform() => transform;
        public Collider2D GetCollider() => _collider2D;
        public float ControlLerp => _currentControlLerp;
        protected override bool CanBeDestroyedOnDestroyCollision() => false;
    }
}