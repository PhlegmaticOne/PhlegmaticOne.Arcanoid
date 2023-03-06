using Game.Behaviors;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class HealthRemoveBehaviour : IObjectBehavior<Ball>
    {
        private float _healthToRemove;
        
        public void SetBehaviourParameters(int healthToRemove)
        {
            _healthToRemove = healthToRemove;
        }
        
        public void Behave(Ball entity, Collision2D collision2D)
        {
            Debug.Log(_healthToRemove);
        }
    }
}