using System;
using UnityEngine;

namespace Common.Data.Models
{
    [Serializable]
    public class BlockData
    {
        [SerializeField] private int blockId;
        [SerializeField] private int bonusId;
        [SerializeField] private int lifesCount;
        
        public int BlockId => blockId;
        public int BonusId => bonusId;
        public int LifesCount => lifesCount;

        public static BlockData Create(int blockId, int bonusId, int lifesCount)
        {
            return new BlockData()
            {
                blockId = blockId,
                bonusId = bonusId,
                lifesCount = lifesCount
            };
        }
    }
}