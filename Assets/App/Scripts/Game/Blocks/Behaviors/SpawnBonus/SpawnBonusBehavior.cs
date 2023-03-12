using Game.Behaviors;
using Game.Bonuses;
using Game.Bonuses.Configurations;
using Game.Bonuses.Spawners;
using UnityEngine;

namespace Game.Blocks.Behaviors.SpawnBonus
{
    public class SpawnBonusBehavior : IObjectBehavior<Block>
    {
        private readonly IBonusSpawner _bonusSpawner;
        private readonly BonusesOnField _bonusesOnField;

        private BonusConfiguration _bonusConfiguration;

        public SpawnBonusBehavior(IBonusSpawner bonusSpawner, BonusesOnField bonusesOnField)
        {
            _bonusSpawner = bonusSpawner;
            _bonusesOnField = bonusesOnField;
        }

        public void SetBehaviorParameters(BonusConfiguration bonusConfiguration) => _bonusConfiguration = bonusConfiguration;

        public void Behave(Block entity, Collision2D collision2D)
        {
            var bonus = _bonusSpawner.SpawnBonus(_bonusConfiguration, new BonusSpawnData
            {
                Position = entity.transform.position
            });
            
            _bonusesOnField.AddBonus(bonus);
            bonus.StartMove();
        }
    }
}