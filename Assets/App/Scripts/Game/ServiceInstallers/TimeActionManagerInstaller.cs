using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class TimeActionManagerInstaller : ServiceInstaller
    {
        [SerializeField] private TimeActionsManager _timeActionsManager;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_timeActionsManager);
        }
    }
}