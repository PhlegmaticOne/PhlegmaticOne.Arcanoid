using System;
using System.Collections.Generic;
using Game.PlayerObjects.Base;
using Libs.InputSystem;
using UnityEngine;

namespace Game.Systems.Control
{
    public class ControlSystem : MonoBehaviour
    {
        [SerializeField] private ControlSystemConfiguration _controlSystemConfiguration;
        
        private readonly List<IStartMovable> _followingObjects = new List<IStartMovable>();
        
        private IDimensionable _baseObjectToMove;
        private Bounds _interactableBounds;
        private Camera _camera;
        
        private IInputSystem _inputSystem;
        private InputData _inputData;
        private Vector3 _startPosition;
        
        public void Initialize(IInputSystem inputSystem, IDimensionable baseObjectToMove, Camera cam)
        {
            _camera = cam;
            _inputSystem = inputSystem;
            _inputSystem.Reset();
            _baseObjectToMove = baseObjectToMove;
            _startPosition = baseObjectToMove.GetTransform().position;
            Enable();
            _inputData = new InputData(baseObjectToMove.GetTransform().position, InputState.None, false);
        }
        
        public void SetInteractableBounds(Bounds interactableBounds) => _interactableBounds = interactableBounds;

        public void AddObjectToFollow(IStartMovable startMovable)
        {
            var basePosition = _baseObjectToMove.GetTransform().position;
            basePosition += new Vector3(0, _baseObjectToMove.GetBounds().extents.y + startMovable.GetBounds().extents.y);
            startMovable.GetTransform().position = basePosition;
            _followingObjects.Add(startMovable);
        }

        public void Enable()
        {
            _inputSystem.Ended += InputSystemOnEnded;
            _baseObjectToMove.GetTransform().position = _startPosition;
        }
        
        public void Disable()
        {
            _inputSystem.Ended -= InputSystemOnEnded;
            _followingObjects.Clear();
        }

        public void DisableInput() => _inputSystem.MakeInvalid();
        public void EnableInput() => _inputSystem.Reset();

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

        private bool NotInBounds(Vector2 newPosition) => _interactableBounds.Contains(newPosition) == false;
        private bool NotValid() => _inputData.IsValid == false;

        private Vector2 UpdateBasePosition(Vector2 newPosition)
        {
            var objTransform = _baseObjectToMove.GetTransform();
            var racketPosition = objTransform.transform.position;
            newPosition.y = racketPosition.y;
            var lerp = Vector3.Lerp(racketPosition, newPosition, _controlSystemConfiguration.Lerp);
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