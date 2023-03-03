using System;
using UnityEngine;

namespace Common.Data.Models
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private int levelId;
        [SerializeField] private int lifesCount;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int[] blocksData;
        
        public int LevelId => levelId;
        public int LifesCount => lifesCount;
        public int Width => width;
        public int Height => height;
        public int[] BlocksData => blocksData;
    }
}

