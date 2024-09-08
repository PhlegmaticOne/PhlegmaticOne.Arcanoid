using Game.GameEntities.Blocks;
using Game.GameEntities.Bonuses.Configurations;
using Game.GameEntities.Bonuses.Spawners.Configurations;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Spawners
{
    public class BonusSpawner : IBonusSpawner
    {
        private readonly IObjectPool<Bonus> _bonusPool;
        private readonly BonusSpawnSystemConfiguration _bonusSpawnSystemConfiguration;
        private readonly Transform _spawnTransform;
        private readonly Block _baseBlock;

        public BonusSpawner(IPoolProvider poolProvider,
            BonusSpawnSystemConfiguration bonusSpawnSystemConfiguration,
            Transform spawnTransform,
            Block baseBlock)
        {
            _bonusPool = poolProvider.GetPool<Bonus>();
            _bonusSpawnSystemConfiguration = bonusSpawnSystemConfiguration;
            _spawnTransform = spawnTransform;
            _baseBlock = baseBlock;
        }

        public Bonus SpawnBonus(BonusConfiguration bonusConfiguration, BonusSpawnData bonusSpawnData)
        {
            var bonusSpawnConfiguration = _bonusSpawnSystemConfiguration.FindBonusConfiguration(bonusConfiguration);
            
            var bonusBehaviorInstaller = bonusSpawnConfiguration.BonusBehaviorInstaller;
            
            var bonus = _bonusPool.Get();
            bonus.transform.SetParent(_spawnTransform);
            bonus.transform.position = bonusSpawnData.Position;
            bonus.Initialize(bonusConfiguration);
            
            var ratio = bonusSpawnData.DestroyedBlockSize.x / _baseBlock.GetBaseSize().x;
            
            if (ratio < 1)
            {
                bonus.MultiplySize(ratio);
            }

            bonusBehaviorInstaller.InstallCollisionBehaviours(bonus);
            bonusBehaviorInstaller.InstallDestroyBehaviours(bonus);
            
            return bonus;
        }
    }
}