using App.Scripts;
using Game.PlayerObjects;
using Libs.InputSystem;
using UnityEngine;

namespace Game.Systems
{
    public class ControlSystem : MonoBehaviour
    {
        [SerializeField] private Racket _racket;
        [SerializeField] private Camera _camera;
        [SerializeField] [Range(0f, 1f)] private float _lerp;
        private Bounds _interactableBounds;
        
        private IInputSystem _inputSystem;
        private InputData _inputData;
        
        public void Initialize(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.Ended += InputSystemOnEnded;
            _inputData = new InputData(_racket.transform.position, InputState.None, false);
        }

        private void InputSystemOnEnded()
        {
            
        }

        public void SetInteractableBounds(Bounds interactableBounds)
        {
            _interactableBounds = interactableBounds;
        }
        
        private void Update()
        {
            _inputData = _inputSystem.ReadInput();
        }

        private void FixedUpdate()
        {
            if (_inputData.IsValid == false)
            {
                return;
            }
            
            var racketPosition = _racket.transform.position;
            var newPosition = ToWorldPoint();

            // if (_interactableBounds.Contains(newPosition) == false)
            // {
            //     return;
            // }
            
            newPosition.y = racketPosition.y;
            var lerp = Vector3.Lerp(racketPosition, newPosition, _lerp);
            _racket.transform.position = lerp;
        }
        
        private Vector3 ToWorldPoint()
        {
            var position = _camera.ScreenToWorldPoint(_inputData.Position);
            position.z = 0;
            return position;
        }
    }
}