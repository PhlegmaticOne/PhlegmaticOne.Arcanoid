namespace Common.Energy.Events
{
    public class TimeChangedModel
    {
        public TimeChangedModel(int timeToNextEnergyInSeconds) => TimeToNextEnergyInSeconds = timeToNextEnergyInSeconds;

        public int TimeToNextEnergyInSeconds { get; }
    }
}