using Game.Logic.Systems.Health;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class HealthRemoveBehaviour : IObjectBehavior<Ball>
    {
        private readonly HealthSystem _healthSystem;
        private float _healthToRemove;

        public HealthRemoveBehaviour(HealthSystem healthSystem) => _healthSystem = healthSystem;
        public bool IsDefault => true;

        public void SetBehaviourParameters(int healthToRemove) => _healthToRemove = healthToRemove;

        public void Behave(Ball entity, Collision2D collision2D) => _healthSystem.LoseHealth();
    }
}