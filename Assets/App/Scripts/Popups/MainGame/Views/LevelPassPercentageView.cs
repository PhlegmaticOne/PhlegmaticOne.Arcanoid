using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Popups.MainGame.Views
{
    public class LevelPassPercentageView : MonoBehaviour
    {
        private const string Percentage = "%";
        [SerializeField] private TextMeshProUGUI _percentageText;
        [SerializeField] private float _updateDuration = 0.5f;
        [SerializeField] private float _minStep;
        private bool _isUpdate;

        private int _currentPercentage;
        private float _previousPercentage;

        public void SetInNormalizedPercentageAnimate(float percentage)
        {
            _isUpdate = true;
            _currentPercentage = GetPercentage(percentage);
        }
        
        public void SetInNormalizedPercentageInstant(float percentage)
        {
            var calculated = GetPercentage(percentage);
            _percentageText.text = Format(calculated);
            _previousPercentage = calculated;
            _currentPercentage = calculated;
        }

        private void Update()
        {
            if (_isUpdate == false)
            {
                return;
            }
            
            var fps = 1.0f / Time.unscaledDeltaTime;
            var step = (_currentPercentage - _previousPercentage) / (fps * _updateDuration);
            
            if(_previousPercentage < _currentPercentage)
            {
                if (step <= _minStep)
                {
                    _previousPercentage = _currentPercentage;
                    _isUpdate = false;
                }
                else
                {
                    _previousPercentage += step;
                }

                _percentageText.text = Format((int)Math.Ceiling(_previousPercentage));
            }
        }
        
        private static int GetPercentage(float normalized) => (int)(normalized * 100);
        private static string Format(int percentage) => percentage + Percentage;
    }
}