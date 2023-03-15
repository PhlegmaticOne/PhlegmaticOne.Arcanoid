using Common.Scenes;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private ColliderTag _bottomColliderTag;
        [SerializeField] private float _actionTime;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var global = ServiceProviderAccessor.Global;
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);

            var balls = gameServices.GetRequiredService<BallsOnField>();
            var pool = global.GetRequiredService<IPoolProvider>();
            var timeManager = gameServices.GetRequiredService<TimeActionsManager>();
            var ballSpawner = gameServices.GetRequiredService<IBallSpawner>();

            var behavior = new CaptiveBallBehavior(timeManager, ballSpawner, pool, balls, _bottomColliderTag);
            behavior.SetBehaviorParameters(_actionTime);
            return behavior;
        }
    }
}