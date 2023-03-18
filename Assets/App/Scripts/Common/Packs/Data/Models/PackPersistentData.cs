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
        
        public bool IsPassed => levelsCount == passedLevelsCount;
    }
}