using UnityEngine;

namespace Scenes.MainGameScene.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack configuration", order = 0)]
    public class PackConfiguration : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private int _order;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _packImage;
        [SerializeField] private int _levelCollectionId;
        [SerializeField] private int _levelsCount;
        [SerializeField] private int _passedLevelsCount;
        [SerializeField] private Color _packColor;

        public int Id => _id;
        public int LevelCollectionId => _levelCollectionId;
        public int Order => _order;
        public string Name => _name;
        public Sprite PackImage => _packImage;
        public int LevelsCount => _levelsCount;
        public int PassedLevelsCount => _passedLevelsCount;
        public Color PackColor => _packColor;

        public void SetLevelsCount(int levelsCount) => _levelsCount = levelsCount;
        
        public void IncreasePassedLevelsCount() => ++_passedLevelsCount;
    }
}

