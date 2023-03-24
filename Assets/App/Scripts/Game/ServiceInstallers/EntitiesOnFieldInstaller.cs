using Game.Composites;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bullets;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class EntitiesOnFieldInstaller : ServiceInstaller
    {
        [SerializeField] private BallsOnField _ballsOnField;
        [SerializeField] private BonusesOnField _bonusesOnField;
        [SerializeField] private BulletsOnField _bulletsOnField;
        [SerializeField] private Ship _ship;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(
                new EntitiesOnFieldCollection(_ballsOnField, _bonusesOnField, _bulletsOnField, _ship));
            serviceCollection.AddSingleton(_ballsOnField);
            serviceCollection.AddSingleton(_bonusesOnField);
            serviceCollection.AddSingleton(_bulletsOnField);
            serviceCollection.AddSingleton(_ship);
        }
    }
}