using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Bonuses;
using Game.Bonuses.Configurations;
using Game.Bonuses.Spawners;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.SpawnBonus
{
    public class SpawnBonusBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private BonusConfiguration _bonusConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var bonusSpawner = gameServices.GetRequiredService<IBonusSpawner>();
            var bonusesOnField = gameServices.GetRequiredService<BonusesOnField>();
            var behavior = new SpawnBonusBehavior(bonusSpawner, bonusesOnField);
            behavior.SetBehaviorParameters(_bonusConfiguration);
            return behavior;
        }
    }
}