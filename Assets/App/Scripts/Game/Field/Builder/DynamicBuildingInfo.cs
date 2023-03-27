using System;
using UnityEngine;

namespace Game.Field.Builder
{
    [Serializable]
    public class DynamicBuildingInfo
    {
        [SerializeField] private int _maxBlocksCountToBuildWithInterval;
        [SerializeField] private float _interval;
        [SerializeField] private float _totalBuildingTime;

        public int MaxBlocksCountToBuildWithInterval => _maxBlocksCountToBuildWithInterval;
        public float Interval => _interval;
        public float TotalBuildingTime => _totalBuildingTime;
    }
}