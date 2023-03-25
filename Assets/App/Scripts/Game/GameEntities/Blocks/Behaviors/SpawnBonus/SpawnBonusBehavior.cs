using Game.GameEntities.Bonuses;
using Game.GameEntities.Bonuses.Configurations;
using Game.GameEntities.Bonuses.Spawners;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.SpawnBonus
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
        
        public bool IsDefault => false;

        public void SetBehaviorParameters(BonusConfiguration bonusConfiguration) => _bonusConfiguration = bonusConfiguration;

        public void Behave(Block entity, Collision2D collision2D)
        {
            var bonus = _bonusSpawner.SpawnBonus(_bonusConfiguration, new BonusSpawnData
            {
                Position = entity.transform.position
            });
            
            _bonusesOnField.Add(bonus);
            bonus.StartMove();
        }
    }
}