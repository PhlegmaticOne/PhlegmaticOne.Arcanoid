using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Behaviors.Common
{
    public class ReturnBonusToPoolBehavior : IObjectBehavior<Bonus>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly BonusesOnField _bonusesOnField;

        public ReturnBonusToPoolBehavior(IPoolProvider poolProvider, BonusesOnField bonusesOnField)
        {
            _poolProvider = poolProvider;
            _bonusesOnField = bonusesOnField;
        }
        
        public bool IsDefault => true;
        
        public void Behave(Bonus entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Bonus>();
            pool.ReturnToPool(entity);
            _bonusesOnField.Remove(entity);
        }
    }
}