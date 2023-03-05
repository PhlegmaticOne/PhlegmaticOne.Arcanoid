using UnityEngine;

namespace Libs.InputSystem
{
    public class InputSystemInstaller : MonoBehaviour
    {
        private InputSystemFactory _inputSystemFactory;
        public IInputSystemFactory Create() => _inputSystemFactory ??= new InputSystemFactory();
    }
}