using System;
using UnityEngine;

namespace Game.Common
{
    [Serializable]
    public class DynamicBlockAffectingInfo
    {
        [SerializeField] private float _interval;
        [SerializeField] private float _maxBuildingTime;

        public float Interval => _interval;
        public float MaxBuildingTime => _maxBuildingTime;

        public float GetAffectingInterval(int actionsCount)
        {
            var maxActions = (int)(_maxBuildingTime / _interval);
            var interval = actionsCount > maxActions ? _maxBuildingTime / actionsCount : _interval;
            return interval;
        }
    }
}