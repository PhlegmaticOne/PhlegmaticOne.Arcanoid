using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Bonuses.Configurations
{
    public class BonusSpawnSystemConfiguration : MonoBehaviour
    {
        [SerializeField] private List<BonusSpawnConfiguration> _bonusConfigurations;
        
        public List<BonusSpawnConfiguration> BonusConfigurations => _bonusConfigurations;

        public BonusSpawnConfiguration FindBonusConfiguration(BonusConfiguration bonusConfiguration)
        {
            var configuration = _bonusConfigurations.FirstOrDefault(x => x.BonusConfiguration == bonusConfiguration);
            return configuration;
        }
    }
}