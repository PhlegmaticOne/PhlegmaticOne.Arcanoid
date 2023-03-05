using UnityEngine;
using UnityEngine.Events;

namespace Libs.InputSystem
{
    public interface IInputSystem
    {
        event UnityAction Began;
        event UnityAction Ended;
        event UnityAction<Vector3> Moved; 
        bool IsValid { get; }
        InputData ReadInput();
        void MakeInvalid();
        void Reset();
    }
}