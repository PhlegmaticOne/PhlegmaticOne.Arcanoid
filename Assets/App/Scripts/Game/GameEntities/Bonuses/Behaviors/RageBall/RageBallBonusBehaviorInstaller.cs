using Common.Scenes;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.RageBall
{
    public class RageBallBonusBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private float _actionTime;
        [SerializeField] private ColliderTag _blockColliderTag;
        
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var balls = gameServices.GetRequiredService<BallsOnField>();
            var timeActionsManager = gameServices.GetRequiredService<TimeActionsManager>();

            var behavior = new RageBallBonusBehavior(timeActionsManager, balls, _blockColliderTag);
            behavior.SetBehaviorParameters(_actionTime);
            return behavior;
        }
    }
}