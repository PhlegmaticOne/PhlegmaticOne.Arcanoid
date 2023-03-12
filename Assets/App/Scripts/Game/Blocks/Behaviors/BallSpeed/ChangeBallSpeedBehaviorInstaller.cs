using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Game.PlayerObjects.BallObject;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.BallSpeed
{
    public class ChangeBallSpeedBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _increaseBallSpeed;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var serviceProvider = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var gameField = serviceProvider.GetRequiredService<GameField>();
            var balls = serviceProvider.GetRequiredService<BallsOnField>();
            var behavior = new ChangeBallSpeedBehavior(gameField, balls);
            behavior.SetBehaviorParameters(_increaseBallSpeed);
            return behavior;
        }
    }
}