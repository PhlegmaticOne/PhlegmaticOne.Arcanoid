using UnityEngine;

namespace Common.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack collection configuration", order = 0)]
    public class PackCollectionConfiguration : ScriptableObject
    {
        [SerializeField] private string _packCollectionSourcePath;
        [SerializeField] private string _levelsSubfolderName;
        [SerializeField] private bool _packsInitialized;
        public string PackCollectionSourcePath => _packCollectionSourcePath;
        public string LevelsSubfolderName => _levelsSubfolderName;
        public bool PacksInitialized => _packsInitialized;
        public bool MarkPacksInitialized() => _packsInitialized = true;
    }
}