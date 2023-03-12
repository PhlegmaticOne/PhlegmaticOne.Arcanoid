using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Game.PlayerObjects.BallObject;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.IncreaseBallSpeed
{
    public class IncreaseBallSpeedBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _increaseBallSpeed;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var serviceProvider = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var gameField = serviceProvider.GetRequiredService<GameField>();
            var balls = serviceProvider.GetRequiredService<BallsOnField>();
            var behavior = new IncreaseBallSpeedBehavior(gameField, balls);
            behavior.SetBehaviorParameters(_increaseBallSpeed);
            return behavior;
        }
    }
}