using System.Collections.Generic;
using Game.GameEntities.PlayerObjects.Base;
using Libs.InputSystem;
using UnityEngine;

namespace Game.Logic.Systems.Control
{
    public class ControlSystem : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private readonly List<IStartMovable> _followingObjects = new List<IStartMovable>();
        private IControlable _baseObjectToMove;
        private Bounds _interactableBounds;
        
        private IInputSystem _inputSystem;
        private InputData _inputData;
        private Vector3 _startPosition;
        
        public void Initialize(IInputSystem inputSystem, IControlable baseObjectToMove)
        {
            _inputSystem = inputSystem;
            _inputSystem.Reset();
            _baseObjectToMove = baseObjectToMove;
            _startPosition = baseObjectToMove.GetTransform().position;
            _inputData = new InputData(baseObjectToMove.GetTransform().position, InputState.None, false);
        }
        
        public void SetInteractableBounds(Bounds interactableBounds) => _interactableBounds = interactableBounds;

        public void AddObjectToFollow(IStartMovable startMovable)
        {
            var baseCollider = _baseObjectToMove.GetCollider();
            var basePosition = (Vector2)_baseObjectToMove.GetTransform().position +
                               baseCollider.offset;
            
            basePosition += new Vector2(0, baseCollider.bounds.extents.y + startMovable.GetCollider().bounds.size.y);
            startMovable.GetTransform().position = basePosition;
            _followingObjects.Add(startMovable);
        }

        public void Enable()
        {
            _inputSystem.Ended += InputSystemOnEnded;
            EnableInput();
            _baseObjectToMove.GetTransform().position = _startPosition;
        }
        
        public void Disable()
        {
            _inputSystem.Ended -= InputSystemOnEnded;
            DisableInput();
            _followingObjects.Clear();
        }

        public void DisableInput() => _inputSystem.MakeInvalid();
        public void EnableInput() => _inputSystem.Reset();
        
        private void Update() => _inputData = _inputSystem.ReadInput();

        private void FixedUpdate()
        {
            if (NotValid())
            {
                return;
            }
            
            var newPosition = ToWorldPoint();

            if (NotInBounds(newPosition))
            {
                return;
            }

            var newBasePosition = UpdateBasePosition(newPosition);
            UpdateFollowingObjects(newBasePosition);
        }
        
        private void InputSystemOnEnded()
        {
            if (NotInBounds(ToWorldPoint()) || NotValid())
            {
                return;
            }
            
            foreach (var followingObject in _followingObjects)
            {
                followingObject.StartMove();
            }
            
            _followingObjects.Clear();
        }

        private bool NotInBounds(Vector2 newPosition) => _interactableBounds.Contains(newPosition) == false;
        private bool NotValid() => _inputData.IsValid == false;

        private Vector2 UpdateBasePosition(Vector2 newPosition)
        {
            var objTransform = _baseObjectToMove.GetTransform();
            var racketPosition = objTransform.transform.position;
            newPosition.y = racketPosition.y;
            var lerp = Vector3.Lerp(racketPosition, newPosition, _baseObjectToMove.ControlLerp);
            objTransform.transform.position = lerp;
            return lerp;
        }

        private void UpdateFollowingObjects(Vector2 newPosition)
        {
            foreach (var followingObject in _followingObjects)
            {
                var objTransform = followingObject.GetTransform();
                var pos = objTransform.position;
                pos.x = newPosition.x;
                objTransform.position = pos;
            }
        }
        
        private Vector3 ToWorldPoint()
        {
            var position = _camera.ScreenToWorldPoint(_inputData.Position);
            position.z = 0;
            return position;
        }
    }
}