using UnityEngine;

namespace Scenes.MainGameScene.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack collection configuration", order = 0)]
    public class PackCollectionConfiguration : ScriptableObject
    {
        [SerializeField] private string _packCollectionSourcePath;
        [SerializeField] private string _levelsSubfolderName;
        public string PackCollectionSourcePath => _packCollectionSourcePath;
        public string LevelsSubfolderName => _levelsSubfolderName;
    }
}