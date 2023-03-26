using Common.Scenes;
using Game.Logic.Systems.Health;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class HealthRemoveBehaviourInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private int _healthToRemove;
        
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var game = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var healthSystem = game.GetRequiredService<HealthSystem>();
            var behaviour = new HealthRemoveBehaviour(healthSystem);
            behaviour.SetBehaviourParameters(_healthToRemove);
            return behaviour;
        }
    }
}