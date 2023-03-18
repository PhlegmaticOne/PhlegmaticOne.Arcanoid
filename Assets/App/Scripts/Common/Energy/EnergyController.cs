using System;
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
            _energyView.SetEnergyInstant(args.CurrentEnergy, args.MaxEnergy);
        }

        private void EnergyManagerOnTimeChanged(TimeChangedModel timeChangedEventArgs)
        {
            _energyView.SetTime(timeChangedEventArgs.TimeToNextEnergyInSeconds);
        }

        public void SpendEnergy(int energy, Action onAnimationPlayed = null)
        {
            var energyChangedModel = _energyManager.SpendEnergy(energy);
            _energyView.SetEnergyAnimate(energyChangedModel.CurrentEnergy, energyChangedModel.MaxEnergy,
                onAnimationPlayed);
        }

        public void AddEnergy(int energy, Action onAnimationPlayed = null)
        {
            var energyChangedModel = _energyManager.AddEnergy(energy);
            _energyView.SetEnergyAnimate(energyChangedModel.CurrentEnergy, energyChangedModel.MaxEnergy,
                onAnimationPlayed);
        }

        public void Disable()
        {
            _energyManager.TimeChanged -= EnergyManagerOnTimeChanged;
            _energyManager.EnergyChangedFromTime -= EnergyManagerOnEnergyChangedFromTime;
        }
    }
}