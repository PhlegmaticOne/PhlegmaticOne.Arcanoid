using UnityEngine;

namespace Common.Packs.Configurations
{
    [CreateAssetMenu(menuName = "Packs/Create pack configuration", order = 0)]
    public class PackConfiguration : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _startLevelEnergy;
        [SerializeField] private int _winLevelEnergy;
        [SerializeField] private Sprite _packImage;
        [SerializeField] private Color _packColor;

        public string Name => _name;
        public int StartLevelEnergy => _startLevelEnergy;
        public int WinLevelEnergy => _winLevelEnergy;
        public Sprite PackImage => _packImage;
        public Color PackColor => _packColor;
    }
}

