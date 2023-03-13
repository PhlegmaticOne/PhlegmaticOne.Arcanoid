using Common.Scenes;
using Game.Field;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.IncreaseBallSpeed
{
    public class IncreaseBallSpeedOnDestroyBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _increaseBallSpeed;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var serviceProvider = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var gameField = serviceProvider.GetRequiredService<GameField>();
            var balls = serviceProvider.GetRequiredService<BallsOnField>();
            var behavior = new IncreaseBallSpeedOnDestroyBehavior(gameField, balls);
            behavior.SetBehaviorParameters(_increaseBallSpeed);
            return behavior;
        }
    }
}