using System;
using UnityEngine;
using UnityEngine.Events;

namespace Libs.InputSystem
{
    public class TouchInputSystem : IInputSystem
    {
        public event UnityAction Began;
        public event UnityAction Ended;
        public event UnityAction<Vector3> Moved;

        public bool IsValid { get; private set; } = true;

        public InputData ReadInput()
        {
            try
            {
                var state = InputState.Active;
                var touch = Input.GetTouch(0);
                var position = touch.position;


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnBegan();
                        state = InputState.Started;
                        break;
                    case TouchPhase.Moved:
                        OnMoved(position);
                        state = InputState.Active;
                        break;
                    case TouchPhase.Ended:
                        OnEnded();
                        state = InputState.Ended;
                        break;
                }

                var isValid = Input.touchCount <= 1;
            
                return IsValid ? new InputData(position, state, isValid) : InputData.Invalid;
            }
            catch (Exception)
            {
                return InputData.Invalid;
            }
        }
        private void OnBegan() => Began?.Invoke();
        private void OnEnded() => Ended?.Invoke();
        private void OnMoved(Vector3 position) => Moved?.Invoke(position);
        public void MakeInvalid() => IsValid = false;
        public void Reset() => IsValid = true;
    }
}