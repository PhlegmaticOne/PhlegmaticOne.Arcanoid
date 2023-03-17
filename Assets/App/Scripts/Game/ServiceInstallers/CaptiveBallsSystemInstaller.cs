using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class CaptiveBallsSystemInstaller : ServiceInstaller
    {
        [SerializeField] private CaptiveBallsSystem _captiveBallsSystem;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_captiveBallsSystem);
        }
    }
}