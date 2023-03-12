using Common.Scenes;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.IncreaseBallsSpeed
{
    public class IncreaseBallsSpeedBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private float _actionTime;
        [SerializeField] private float _speedToAdd;
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var ballsOnField = gameServices.GetRequiredService<BallsOnField>();
            var timeActionsManager = gameServices.GetRequiredService<TimeActionsManager>();
            var behavior = new IncreaseBallsSpeedBehavior(ballsOnField, timeActionsManager);
            behavior.SetBehaviorParameters(_speedToAdd, _actionTime);
            return behavior;
        }
    }
}