using System.Collections.Generic;
using Game.PlayerObjects.Base;
using Libs.InputSystem;
using UnityEngine;

namespace Game.Systems.Control
{
    public class ControlSystem : MonoBehaviour
    {
        [SerializeField] private ControlSystemConfiguration _controlSystemConfiguration;
        [SerializeField] private Camera _camera;
        
        private readonly List<IStartMovable> _followingObjects = new List<IStartMovable>();
        
        private IDimensionable _baseObjectToMove;
        private Bounds _interactableBounds;
        
        private IInputSystem _inputSystem;
        private InputData _inputData;
        
        public void Initialize(IInputSystem inputSystem, IDimensionable baseObjectToMove)
        {
            _inputSystem = inputSystem;
            _baseObjectToMove = baseObjectToMove;
            _inputSystem.Ended += InputSystemOnEnded;
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

        private void InputSystemOnEnded()
        {
            foreach (var followingObject in _followingObjects)
            {
                followingObject.StartMove();
            }
            _followingObjects.Clear();
        }

        private void Update() => _inputData = _inputSystem.ReadInput();

        private void FixedUpdate()
        {
            if (_inputData.IsValid == false)
            {
                return;
            }
            
            var newPosition = ToWorldPoint();

            if (_interactableBounds.Contains((Vector2)newPosition) == false)
            {
                return;
            }

            var newBasePosition = UpdateBasePosition(newPosition);
            UpdateFollowingObjects(newBasePosition);
        }

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