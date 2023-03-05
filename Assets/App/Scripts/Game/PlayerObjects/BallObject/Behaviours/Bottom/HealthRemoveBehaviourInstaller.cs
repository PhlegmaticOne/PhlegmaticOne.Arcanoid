using Game.Behaviors;
using Game.Behaviors.Installer;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviours.Bottom
{
    public class HealthRemoveBehaviourInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private int _healthToRemove;
        
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behaviour = new HealthRemoveBehaviour();
            behaviour.SetBehaviourParameters(_healthToRemove);
            return behaviour;
        }
    }
}