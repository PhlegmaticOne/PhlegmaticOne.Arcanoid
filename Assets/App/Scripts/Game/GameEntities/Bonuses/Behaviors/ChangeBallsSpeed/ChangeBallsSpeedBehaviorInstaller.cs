using Common.Scenes;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeBallsSpeed
{
    public class ChangeBallsSpeedBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private float _actionTime;
        [SerializeField] private float _speedToChange;
        [SerializeField] private bool _isAdding;
        
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var ballsOnField = gameServices.GetRequiredService<BallsOnField>();
            var timeActionsManager = gameServices.GetRequiredService<TimeActionsManager>();
            var behavior = new ChangeBallsSpeedBehavior(ballsOnField, timeActionsManager);
            behavior.SetBehaviorParameters(_speedToChange, _actionTime, _isAdding);
            return behavior;
        }
    }
}