using Common.Scenes;
using Game.GameEntities.Bullets;
using Game.GameEntities.Bullets.Spawner;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.Shotgun
{
    public class ShotgunBonusBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private float _actionTime;
        [SerializeField] private float _shootInterval;
        
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var bulletSpawner = gameServices.GetRequiredService<IBulletSpawner>();
            var timeActionsManager = gameServices.GetRequiredService<TimeActionsManager>();
            var ship = gameServices.GetRequiredService<Ship>();
            var bulletsOnField = gameServices.GetRequiredService<BulletsOnField>();

            var behavior = new ShotgunBonusBehavior(timeActionsManager, bulletsOnField, bulletSpawner, ship);
            behavior.SetBehaviorParameters(_actionTime, _shootInterval);
            return behavior;
        }
    }
}