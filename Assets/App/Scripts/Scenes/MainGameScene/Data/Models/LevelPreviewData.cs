using System;

namespace Scenes.MainGameScene.Data
{
    [Serializable]
    public class LevelPreviewData 
    {
        public int LevelId { get; }
        public bool IsCompleted { get; private set; }

        public LevelPreviewData(int levelId, bool isCompleted)
        {
            LevelId = levelId;
            IsCompleted = isCompleted;
        }

        public void Pass() => IsCompleted = true;
    }
}
