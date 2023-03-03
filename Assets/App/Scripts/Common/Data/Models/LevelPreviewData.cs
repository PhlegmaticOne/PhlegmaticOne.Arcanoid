using System;

namespace Common.Data.Models
{
    [Serializable]
    public class LevelPreviewData 
    {
        public int LevelId { get; }
        public string PackName { get; }
        public bool IsCompleted { get; private set; }

        public LevelPreviewData(int levelId, string packName, bool isCompleted)
        {
            LevelId = levelId;
            PackName = packName;
            IsCompleted = isCompleted;
        }

        public void Pass() => IsCompleted = true;
    }
}
