using UnityEngine;

namespace Common.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack configuration", order = 0)]
    public class PackConfiguration : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _packImage;
        [SerializeField] private Color _packColor;

        public string Name => _name;
        public Sprite PackImage => _packImage;
        public Color PackColor => _packColor;
    }
}

