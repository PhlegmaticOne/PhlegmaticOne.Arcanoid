namespace Common.Energy.Models
{
    public class EnergyViewModel
    {
        public EnergyViewModel(int maxEnergy, int currentEnergy)
        {
            MaxEnergy = maxEnergy;
            CurrentEnergy = currentEnergy;
        }

        public int MaxEnergy { get; }
        public int CurrentEnergy { get; }
    }
}