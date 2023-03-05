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
        [SerializeField] private BlockData[] blocksData;
        
        public int LevelId => levelId;
        public int LifesCount => lifesCount;
        public int Width => width;
        public int Height => height;
        public BlockData[] BlocksData => blocksData;

        public static LevelData Create(int levelId, int lifesCount, int width, int height, BlockData[] blockData)
        {
            var result = new LevelData
            {
                levelId = levelId,
                lifesCount = lifesCount,
                width = width,
                height = height,
                blocksData = blockData
            };

            return result;
        }
    }
}

