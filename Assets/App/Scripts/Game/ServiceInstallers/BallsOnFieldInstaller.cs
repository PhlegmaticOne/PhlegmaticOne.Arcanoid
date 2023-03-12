using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BallsOnFieldInstaller : ServiceInstaller
    {
        [SerializeField] private BallsOnField _ballsOnField;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_ballsOnField);
        }
    }
}