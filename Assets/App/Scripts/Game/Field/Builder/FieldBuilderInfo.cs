using System;
using UnityEngine;

namespace Game.Field.Builder
{
    [CreateAssetMenu(menuName = "Field/Create field building animation configuration")]
    public class FieldBuilderInfo : ScriptableObject
    {
        [SerializeField] private float _scalePunchTime;
        [SerializeField] private float _maxTimeToBuild;
        [SerializeField] private float _defaultScaleInterval;

        public float ScalePunchTime => _scalePunchTime;

        public float GetIntervalTime(int count)
        {
            var maxActions = (int)(_maxTimeToBuild / _defaultScaleInterval);
            var interval = count > maxActions ? _maxTimeToBuild / count : _defaultScaleInterval;
            return interval; 
        }
    }
}