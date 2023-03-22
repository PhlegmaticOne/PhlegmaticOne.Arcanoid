using System;
using UnityEngine;

namespace Common.Packs.Data.Models
{
    [Serializable]
    public class PackPersistentData
    {
        public const int Passed = -1;
        
        [SerializeField] public string name;
        [SerializeField] public int levelsCount;
        [SerializeField] public int passedLevelsCount;
        [SerializeField] public int currentLevelId;
        [SerializeField] public bool isPassed;
        [SerializeField] public bool isOpened;

        public bool HasNextLevel() => passedLevelsCount < levelsCount - 1;
        public bool IsCurrentlyPassed() => passedLevelsCount == levelsCount - 1;
        public bool IsPassedFirstTime() => IsCurrentlyPassed() && isPassed == false;
        public bool IsPassedNotFirstTime() => IsCurrentlyPassed() && isPassed;
    }
}