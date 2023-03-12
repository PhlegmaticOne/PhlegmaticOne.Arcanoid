using Game.Logic.Systems.Control;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class ControlSystemInstaller : ServiceInstaller
    {
        [SerializeField] private ControlSystem _controlSystem;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_controlSystem);
        }
    }
}