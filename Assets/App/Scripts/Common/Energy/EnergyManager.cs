using Common.Energy.Events;
using Common.Energy.Models;
using Common.Energy.Repositories;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Energy
{
    public class EnergyManager : MonoBehaviour
    {
        private IEnergyRepository _energyRepository;
        private EnergyModel _energyModel;
        private bool _isUpdating;
        
        private int _regenerationTimeInSeconds;
        private int _secondsPassed;
        private float _currentTimeInSeconds;
        private void Awake() => DontDestroyOnLoad(gameObject);

        public void Initialize(IEnergyRepository energyRepository)
        {
            _energyRepository = energyRepository;
            _energyModel = energyRepository.GetEnergyModel();
            _regenerationTimeInSeconds = (int)(_energyModel.regenerationTimeInMinutes * 60);
            UpdateIsUpdating();
        }

        public bool IsTimeRegenerating => _isUpdating;

        public event UnityAction<EnergyChangedModel> EnergyChangedFromTime;
        public event UnityAction<TimeChangedModel> TimeChanged;

        private void Update()
        {
            if (_isUpdating == false)
            {
                return;
            }

            _currentTimeInSeconds += Time.unscaledDeltaTime;

            if (_currentTimeInSeconds - _secondsPassed >= 1)
            {
                IncreaseSecondsPassed();
            }

            if (_currentTimeInSeconds >= _regenerationTimeInSeconds)
            {
                ResetTime();
                ChangeEnergyModelFromTime();
            }
        }

        public EnergyChangedModel AddEnergy(int energy)
        {
            _energyModel.AddEnergy(energy);
            return OnEnergyChanged();
        }

        public EnergyChangedModel SpendEnergy(int energy)
        {
            _energyModel.SpendEnergy(energy);
            return OnEnergyChanged();
        }

        public void Raise()
        {
            RaiseTime();
            RaiseModel();
        }

        public void DisableUpdating() => _isUpdating = false;

        public void EnableUpdating() => UpdateIsUpdating();

        private EnergyChangedModel OnEnergyChanged()
        {
            _energyRepository.Save(_energyModel);
            UpdateIsUpdating();
            return new EnergyChangedModel(_energyModel.maxEnergy, _energyModel.currentEnergy);
        }

        private void ResetTime()
        {
            _currentTimeInSeconds = 0;
            _secondsPassed = 0;
        }

        private void ChangeEnergyModelFromTime()
        {
            _energyModel.IncreaseFromTime();
            _energyRepository.Save(_energyModel);
            UpdateIsUpdating();
            RaiseModel();
        }

        private void IncreaseSecondsPassed()
        {
            ++_secondsPassed;
            RaiseTime();
        }

        private void RaiseTime() => 
            TimeChanged?.Invoke(new TimeChangedModel(_regenerationTimeInSeconds - _secondsPassed));
        private void RaiseModel() => 
            EnergyChangedFromTime?.Invoke(new EnergyChangedModel(_energyModel.maxEnergy, _energyModel.currentEnergy));

        private void UpdateIsUpdating() => _isUpdating = _energyModel.IsFull() == false;
    }
}