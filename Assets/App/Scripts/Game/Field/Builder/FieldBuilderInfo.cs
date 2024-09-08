using System;
using UnityEngine;

namespace Game.Field.Builder
{
    [CreateAssetMenu(menuName = "Field/Create field building animation configuration")]
    public class FieldBuilderInfo : ScriptableObject
    {
        [SerializeField] private float _maxTimeToBuild;
        [SerializeField] private float _defaultScaleInterval;

        [SerializeField] private float _maxScale;
        [SerializeField] private float _timeToMaxScale;
        [SerializeField] private float _timeFromMaxScaleToOne;

        public float MaxScale => _maxScale;
        public float TimeToMaxScale => _timeToMaxScale;
        public float TimeFromMaxScaleToOne => _timeFromMaxScaleToOne;

        public float GetIntervalTime(int count)
        {
            var maxActions = (int)(_maxTimeToBuild / _defaultScaleInterval);
            var interval = count > maxActions ? _maxTimeToBuild / count : _defaultScaleInterval;
            return interval; 
        }
    }
}