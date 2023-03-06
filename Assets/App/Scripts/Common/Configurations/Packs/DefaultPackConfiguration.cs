using UnityEngine;

namespace Common.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create default pack collection configuration")]
    public class DefaultPackConfiguration : ScriptableObject
    {
        [SerializeField] private PackConfiguration _defaultPack;
        [SerializeField] private int _defaultLevelIndex;

        public PackConfiguration DefaultPack => _defaultPack;
        public int DefaultLevelIndex => _defaultLevelIndex;
    }
}