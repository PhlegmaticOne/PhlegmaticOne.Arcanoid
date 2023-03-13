using System;
using Game.Logic.Systems.Health;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeHealth
{
    public class ChangeHealthBehavior : IObjectBehavior<Bonus>
    {
        private readonly HealthSystem _healthSystem;

        private int _healthToChange;
        private bool _isAdding;

        public ChangeHealthBehavior(HealthSystem healthSystem) => _healthSystem = healthSystem;

        public void SetBehaviorParameters(int healthToChange, bool isAdding)
        {
            _healthToChange = healthToChange;
            _isAdding = isAdding;
        }
        
        public void Behave(Bonus entity, Collision2D collision2D)
        {
            for (var i = 0; i < _healthToChange; i++)
            {
                if (_isAdding)
                {
                    _healthSystem.AddHealth();   
                }
                else
                {
                    _healthSystem.LoseHealth();
                }
            }
        }
    }
}