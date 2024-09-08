using Common.Scenes;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.PlatformSpeed
{
    public class ChangePlatformSpeedBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private float _actionTime;
        [SerializeField] private float _changeBy;
        [SerializeField] private bool _isIncrease;
        
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var ship = gameServices.GetRequiredService<Ship>();
            var timeManager = gameServices.GetRequiredService<TimeActionsManager>();

            var behavior = new ChangePlatformSpeedBehavior(timeManager, ship);
            behavior.SetBehaviorParameters(_actionTime, _changeBy, _isIncrease);
            return behavior;
        }
    }
}