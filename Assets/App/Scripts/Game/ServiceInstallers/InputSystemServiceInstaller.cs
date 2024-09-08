using Libs.InputSystem;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class InputSystemServiceInstaller : ServiceInstaller
    {
        [SerializeField] private InputSystemInstaller _inputSystemInstaller;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            var input = _inputSystemInstaller.Create();
            serviceCollection.AddSingleton(input.CreateInput());
        }
    }
}