using System.Collections.Generic;
using System.Linq;
using Scenes.MainGameScene.Data;
using UnityEngine;

namespace Scenes.MainGameScene.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack levels collection", order = 0)]
    public class PackLevelCollection : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private List<LevelPreviewData> _levelPreviews;

        public int Id => _id;
        
        public IReadOnlyList<LevelPreviewData> LevelPreviews => _levelPreviews;
        
        public LevelPreviewData this[int index] => _levelPreviews[index];
        
        public bool IsInitialized => _levelPreviews.Any();

        public void PassLevel(int levelId)
        {
            var levelPreview = ById(levelId);
            if (levelPreview != null)
            {
                levelPreview.Pass();
            }
        }

        public LevelPreviewData GetNextLevel(int currentLevelId)
        {
            var currentLevel = ById(currentLevelId);
            var currentLevelIndex = _levelPreviews.IndexOf(currentLevel);
            
            return currentLevelIndex != _levelPreviews.Count - 1 ? _levelPreviews[++currentLevelId] : null;
        }

        public void Initialize(IEnumerable<LevelPreviewData> levelPreviews)
        {
            _levelPreviews.Clear();
            _levelPreviews.AddRange(levelPreviews);
        }

        private LevelPreviewData ById(int id) => _levelPreviews.First(x => x.LevelId == id);
    }
}