using UnityEngine;

namespace Common.Energy.Configurations
{
    [CreateAssetMenu(menuName = "Energy/Create energy configuration")]
    public class EnergyConfiguration : ScriptableObject
    {
        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _startEnergy;
        [SerializeField] private float _regenerationTimeInMinutes;
        [SerializeField] private int _increaseEnergyFromTimeCount = 1;
        [SerializeField] private string _directoryPersistentPath;
        [SerializeField] private string _fileName;
        
        public int MaxEnergy => _maxEnergy;
        public int IncreaseEnergyFromTimeCount => _increaseEnergyFromTimeCount;
        public int StartEnergy => _startEnergy;
        public float RegenerationTimeInMinutes => _regenerationTimeInMinutes;
        public string DirectoryPersistentPath => _directoryPersistentPath;
        public string FileName => _fileName;
    }
}