using Game.GameEntities.Base;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bullets;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.Behaviors;
using Libs.Pooling.Base;

namespace Game.Composites
{
    public class EntitiesOnFieldCollection
    {
        public BallsOnField BallsOnField { get; }
        public BonusesOnField BonusesOnField { get; }
        public BulletsOnField BulletsOnField { get; }
        public Ship Ship { get; set; }

        public EntitiesOnFieldCollection(BallsOnField ballsOnField,
            BonusesOnField bonusesOnField,
            BulletsOnField bulletsOnField,
            Ship ship)
        {
            Ship = ship;
            BallsOnField = ballsOnField;
            BonusesOnField = bonusesOnField;
            BulletsOnField = bulletsOnField;
        }

        public void ReturnToPool(IPoolProvider poolProvider)
        {
            Return(poolProvider, BallsOnField);
            Return(poolProvider, BonusesOnField);
            Return(poolProvider, BulletsOnField);
        }

        private void Return<T>(IPoolProvider poolProvider, EntitiesOnField<T> entities) 
            where T : BehaviorObject<T>
        {
            var pool = poolProvider.GetPool<T>();
            
            foreach (var entity in entities.All)
            {
                pool.ReturnToPool(entity);    
            }
            
            entities.Clear();
        }
    }
}