using UnityEngine;

namespace Game.Field.Helpers
{
    public class InteractableZoneSetter : MonoBehaviour
    {
        private const float NonDependentColliderSize = 0.1f;
        [SerializeField] private Camera _camera;
        
        [SerializeField] private BoxCollider2D _bottomCollider;
        [SerializeField] private BoxCollider2D _leftSideCollider;
        [SerializeField] private BoxCollider2D _topCollider;
        [SerializeField] private BoxCollider2D _rightSideCollider;

        public Bounds CalculateZoneBounds(Bounds fieldBounds)
        {
            var halfHeight = _camera.orthographicSize;
            var halfWidth = halfHeight * _camera.aspect;

            var topY = fieldBounds.center.y + fieldBounds.extents.y;
            var centerY = (topY - halfHeight) / 2;
            var centerX = 0;

            var horizontalCollidersSize = new Vector2(2 * halfWidth, NonDependentColliderSize);
            var verticalCollidersSize = new Vector2(NonDependentColliderSize, topY + halfHeight);

            var bounds = new Bounds(new Vector3(centerX, centerY),
                new Vector3(horizontalCollidersSize.x, verticalCollidersSize.y));

            return bounds;
        }

        public void SetInteractableZone(Bounds interactableBounds)
        {
            var top = interactableBounds.max;
            var bottom = interactableBounds.min;
            var center = interactableBounds.center;
            var size = interactableBounds.size;
            
            var horizontalCollidersSize = new Vector2(size.x, NonDependentColliderSize);
            var verticalCollidersSize = new Vector2(NonDependentColliderSize, size.y);
            
            var bottomPosition = new Vector2(0, SubtractNonDependent(bottom.y));
            var topPosition = new Vector2(0, AddNonDependent(top.y));
            var leftPosition = new Vector2(SubtractNonDependent(bottom.x), center.y);
            var rightPosition = new Vector2(AddNonDependent(top.x), center.y);

            SetCollider(_bottomCollider, bottomPosition, horizontalCollidersSize);
            SetCollider(_topCollider, topPosition, horizontalCollidersSize);
            SetCollider(_leftSideCollider, leftPosition, verticalCollidersSize);
            SetCollider(_rightSideCollider, rightPosition, verticalCollidersSize);
        }

        private static float SubtractNonDependent(float value) => value - NonDependentColliderSize / 2;
        private static float AddNonDependent(float value) => value + NonDependentColliderSize / 2;

        private static void SetCollider(BoxCollider2D collider2D, Vector3 position, Vector2 size)
        {
            collider2D.transform.position = position;
            collider2D.size = size;
        }
    }
}