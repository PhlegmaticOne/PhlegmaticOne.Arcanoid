using System;
using UnityEngine;

namespace Common.Data.Models
{
    [Serializable]
    public class PackLevelsData
    {
        [SerializeField] public int[] levelIds;

        public int GetIndexOfLevel(int levelId) => Array.IndexOf(levelIds, levelId);
    }
}