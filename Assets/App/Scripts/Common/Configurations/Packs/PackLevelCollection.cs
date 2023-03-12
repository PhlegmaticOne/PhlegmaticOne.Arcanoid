using System.Collections.Generic;
using System.Linq;
using Common.Data.Models;
using UnityEngine;

namespace Common.Configurations.Packs
{
    [CreateAssetMenu(menuName = "Packs/Create pack levels collection", order = 0)]
    public class PackLevelCollection : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private bool _isLevelsInitialized;
        [SerializeField] private string _packName;
        [SerializeField] private List<LevelPreviewData> _levelPreviews;

        public int Id => _id;
        public string PackName => _packName;
        public bool IsLevelsInitialized => _isLevelsInitialized;
        
        public IReadOnlyList<LevelPreviewData> LevelPreviews => _levelPreviews;
        
        public LevelPreviewData this[int index] => _levelPreviews[index];
        
        public void PassLevel(int levelId)
        {
            var levelPreview = ById(levelId);
            
            if (levelPreview != null)
            {
                levelPreview.Pass();
            }
        }

        public int GetLevelOrderInPack(int levelId)
        {
            var level = _levelPreviews.FirstOrDefault(x => x.LevelId == levelId);
            
            if (level == null)
            {
                return -1;
            }

            return _levelPreviews.IndexOf(level);
        }

        public LevelPreviewData GetNextLevel(int currentLevelId)
        {
            var currentLevel = ById(currentLevelId);
            var currentLevelIndex = _levelPreviews.IndexOf(currentLevel);
            
            return currentLevelIndex != _levelPreviews.Count - 1 ? _levelPreviews[++currentLevelId] : null;
        }

        public void Initialize(IEnumerable<LevelPreviewData> levelPreviews, string packName)
        {
            _levelPreviews.Clear();
            _levelPreviews.AddRange(levelPreviews);
            _isLevelsInitialized = true;
            _packName = packName;
        }

        private LevelPreviewData ById(int id) => _levelPreviews.First(x => x.LevelId == id);
    }
}