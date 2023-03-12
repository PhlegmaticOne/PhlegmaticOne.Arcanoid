using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Systems.Health;
using Libs.Services;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class HealthRemoveBehaviourInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private int _healthToRemove;
        
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var game = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var healthSystem = game.GetRequiredService<HealthSystem>();
            var behaviour = new HealthRemoveBehaviour(healthSystem);
            behaviour.SetBehaviourParameters(_healthToRemove);
            return behaviour;
        }
    }
}