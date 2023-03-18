namespace Common.Energy.Events
{
    public class EnergyChangedModel
    {
        public EnergyChangedModel(int maxEnergy, int currentEnergy)
        {
            MaxEnergy = maxEnergy;
            CurrentEnergy = currentEnergy;
        }

        public int MaxEnergy { get; }
        public int CurrentEnergy { get; }
    }
}