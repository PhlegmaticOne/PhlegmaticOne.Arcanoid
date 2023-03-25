using Game.GameEntities.Bullets;
using Game.GameEntities.Bullets.Spawner;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.Shotgun
{
    public class ShotgunBonusBehavior : IObjectBehavior<Bonus>
    {
        private readonly TimeActionsManager _timeActionsManager;
        private readonly BulletsOnField _bulletsOnField;
        private readonly IBulletSpawner _bulletSpawner;
        private readonly Ship _ship;

        private float _actionTime;
        private float _shootInterval;

        public ShotgunBonusBehavior(TimeActionsManager timeActionsManager,
            BulletsOnField bulletsOnField,
            IBulletSpawner bulletSpawner,
            Ship ship)
        {
            _timeActionsManager = timeActionsManager;
            _bulletsOnField = bulletsOnField;
            _bulletSpawner = bulletSpawner;
            _ship = ship;
        }
        
        public bool IsDefault => false;

        public void SetBehaviorParameters(float actionTime, float shootInterval)
        {
            _actionTime = actionTime;
            _shootInterval = shootInterval;
        }
        
        public void Behave(Bonus entity, Collision2D collision2D)
        {
            var actionsCount = (int)(_actionTime / _shootInterval);

            if (_timeActionsManager.TryGetAction<ShotgunTimeAction>(out var action))
            {
                action.Reset();
            }
            else
            {
                _timeActionsManager.AddTimeAction(new ShotgunTimeAction(
                    _bulletsOnField, _bulletSpawner, _ship, _shootInterval, actionsCount));
            }
        }
    }
}