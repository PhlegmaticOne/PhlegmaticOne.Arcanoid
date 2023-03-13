using Game.GameEntities.Bonuses.Configurations;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.Bonuses.Spawners.Configurations
{
    public class BonusSpawnConfiguration : MonoBehaviour
    {
        [SerializeField] private BonusConfiguration _bonusConfiguration;
        [SerializeField] private BehaviorObjectInstaller<Bonus> _bonusBehaviorInstaller;
        
        public BonusConfiguration BonusConfiguration => _bonusConfiguration;
        public BehaviorObjectInstaller<Bonus> BonusBehaviorInstaller => _bonusBehaviorInstaller;
    }
}