using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class ShipInstaller : ServiceInstaller
    {
        [SerializeField] private Ship _ship;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_ship);
        }
    }
}