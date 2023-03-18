using System;

namespace Common.Energy.Models
{
    [Serializable]
    public class EnergyModel
    {
        public int maxEnergy;
        public int currentEnergy;
        public float regenerationTimeInMinutes;
        public int increaseEnergyFromTimeCount;
        public DateTime lastModifiedTime;

        public void AddEnergy(int energy) => currentEnergy += energy;
        
        public void SpendEnergy(int energy)
        {
            currentEnergy -= energy;

            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
            }
        }

        public bool IsFull() => currentEnergy == maxEnergy;

        public void IncreaseFromTime()
        {
            currentEnergy += increaseEnergyFromTimeCount;
            
            if (currentEnergy >= maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }
    }
}