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
    }
}