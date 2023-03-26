using Common.Scenes;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bonuses.Configurations;
using Game.GameEntities.Bonuses.Spawners;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.SpawnBonus
{
    public class SpawnBonusBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private BonusConfiguration _bonusConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var bonusSpawner = gameServices.GetRequiredService<IBonusSpawner>();
            var bonusesOnField = gameServices.GetRequiredService<BonusesOnField>();
            var behavior = new SpawnBonusBehavior(bonusSpawner, bonusesOnField);
            behavior.SetBehaviorParameters(_bonusConfiguration);
            return behavior;
        }
    }
}