using System;
using UnityEngine;

namespace Common.Data.Models
{
    [Serializable]
    public class LevelPreviewData
    {
        [SerializeField] private int _levelId;
        [SerializeField] private bool _isCompleted;
        public int LevelId => _levelId;
        public bool IsCompleted => _isCompleted;

        public LevelPreviewData(int levelId, bool isCompleted)
        {
            _levelId = levelId;
            _isCompleted = isCompleted;
        }
        
        public void Pass() => _isCompleted = true;
    }
}
