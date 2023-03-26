namespace Common.Energy.Events
{
    public class EnergyChangedModel
    {
        public int EnergyChanged { get; }

        public EnergyChangedModel(int energyChanged) => EnergyChanged = energyChanged;
    }
}