using Game.Field.Configurations;
using Game.Systems.Control;
using UnityEngine;

namespace Game.Field.Helpers
{
    public class InteractableZoneSetter : MonoBehaviour
    {
        private const float NonDependentColliderSize = 0.1f;
        private float _screenWidth;
        private float _screenHeight;
        
        [SerializeField] private GameFieldConfiguration _gameFieldConfiguration;
        [SerializeField] private Camera _camera;
        
        [SerializeField] private BoxCollider2D _bottomCollider;
        [SerializeField] private BoxCollider2D _leftSideCollider;
        [SerializeField] private BoxCollider2D _topCollider;
        [SerializeField] private BoxCollider2D _rightSideCollider;

        [SerializeField] private ControlSystem _controlSystem;

        private void Start()
        {
            var fieldBounds = GenerateFieldBounds();
            var zoneBounds = CalculateZoneBounds(fieldBounds);
            SetInteractableZone(zoneBounds);
            _controlSystem.SetInteractableBounds(zoneBounds);
        }

        private Bounds GenerateFieldBounds()
        {
            _screenHeight = Screen.height;
            _screenWidth = Screen.width;
            
            var fieldMargin = _gameFieldConfiguration.FieldMargin;
            
            var topMargin = HeightFromPercentage(fieldMargin.FromTop);
            var bottomMargin = HeightFromPercentage(fieldMargin.FromBottom);
            var rightMargin = WidthFromPercentage(fieldMargin.FromRight);
            var leftMargin = WidthFromPercentage(fieldMargin.FromLeft);
            
            var startPositionScreen = new Vector2(leftMargin, _screenHeight - topMargin);
            var endPositionScreen = new Vector2(_screenWidth - rightMargin, bottomMargin);
            
            var fieldStartPosition = ToWorldPoint(startPositionScreen);
            var fieldEndPosition = ToWorldPoint(endPositionScreen);
            
            var fieldSizeWorld = new Vector2(
                fieldEndPosition.x - fieldStartPosition.x,
                fieldStartPosition.y - fieldEndPosition.y);
            
            var fieldBounds = new Bounds((fieldEndPosition + fieldStartPosition) / 2, fieldSizeWorld);

            return fieldBounds;
        }
        
        private Bounds CalculateZoneBounds(Bounds fieldBounds)
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

        private void SetInteractableZone(Bounds interactableBounds)
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
        
        private float HeightFromPercentage(float percentage) => _screenHeight * percentage;
        private float WidthFromPercentage(float percentage) => _screenWidth * percentage;
        private Vector2 ToWorldPoint(Vector2 vector) => _camera.ScreenToWorldPoint(vector);
        private static float SubtractNonDependent(float value) => value - NonDependentColliderSize / 2;
        private static float AddNonDependent(float value) => value + NonDependentColliderSize / 2;
        private static void SetCollider(BoxCollider2D collider2D, Vector3 position, Vector2 size)
        {
            collider2D.transform.position = position;
            collider2D.size = size;
        }
    }
}