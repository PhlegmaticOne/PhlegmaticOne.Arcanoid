using Game.GameEntities.Bullets;
using Game.GameEntities.Bullets.Spawner;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.Shotgun
{
    public class ShotgunTimeAction : FixedIntervalTimeAction
    {
        private readonly BulletsOnField _bulletsOnField;
        private IBulletSpawner _bulletSpawner;
        private readonly Ship _ship;

        public ShotgunTimeAction(BulletsOnField bulletsOnField, IBulletSpawner bulletSpawner, Ship ship, 
            float fixedInterval, int actionsCount) : 
            base(fixedInterval, actionsCount)
        {
            _bulletsOnField = bulletsOnField;
            _bulletSpawner = bulletSpawner;
            _ship = ship;
        }

        public override void OnStart() { }

        public override void OnEnd()
        {
            _bulletSpawner = null;
        }

        protected override void OnInterval(int interval)
        {
            foreach (var shotgunTransform in _ship.ShotgunTransforms)
            {
                var bullet = _bulletSpawner.CreateBullet(new BulletCreationContext
                {
                    Position = shotgunTransform.position
                });
                _bulletsOnField.Add(bullet);
                bullet.StartMove();
            }
        }
    }
}