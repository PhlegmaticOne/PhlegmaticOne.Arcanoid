using Common.Scenes;
using Game.Logic.Systems.Health;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeHealth
{
    public class ChangeHealthBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        [SerializeField] private int _healthToChange;
        [SerializeField] private bool _isAdding;
        
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var healthSystem = gameServices.GetRequiredService<HealthSystem>();
            var behavior = new ChangeHealthBehavior(healthSystem);
            behavior.SetBehaviorParameters(_healthToChange, _isAdding);
            return behavior;
        }
    }
}