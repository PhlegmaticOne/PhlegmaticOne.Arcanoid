using UnityEngine;

namespace Common.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create default pack collection configuration")]
    public class DefaultPackConfiguration : ScriptableObject
    {
        [SerializeField] private PackConfiguration _defaultPack;
        [SerializeField] private int _defaultLevelId;

        public PackConfiguration DefaultPack => _defaultPack;
        public int DefaultLevelId => _defaultLevelId;
    }
}