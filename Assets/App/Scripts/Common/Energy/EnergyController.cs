using Common.Energy.Events;

namespace Common.Energy
{
    public class EnergyController
    {
        private readonly EnergyManager _energyManager;
        private readonly EnergyView _energyView;

        public EnergyController(EnergyManager energyManager, EnergyView energyView)
        {
            _energyManager = energyManager;
            _energyView = energyView;

            var energyModel = energyManager.EnergyViewModel;
            _energyView.Init(energyModel.CurrentEnergy, energyModel.MaxEnergy);
            _energyManager.TimeChanged += EnergyManagerOnTimeChanged;
            _energyManager.EnergyChangedFromTime += EnergyManagerOnEnergyChangedFromTime;
            _energyManager.Raise();
        }

        private void EnergyManagerOnEnergyChangedFromTime(EnergyChangedModel args)
        {
            if (_energyManager.IsTimeRegenerating == false)
            {
                _energyView.SetIsFull();
            }
            _energyView.ChangeEnergyInstant(args.EnergyChanged);
        }

        private void EnergyManagerOnTimeChanged(TimeChangedModel timeChangedEventArgs)
        {
            if (_energyManager.IsTimeRegenerating == false)
            {
                _energyView.SetIsFull();
                return;
            }
            _energyView.SetTime(timeChangedEventArgs.TimeToNextEnergyInSeconds);
        }
        
        public void Disable()
        {
            _energyManager.TimeChanged -= EnergyManagerOnTimeChanged;
            _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
        }
    }
}