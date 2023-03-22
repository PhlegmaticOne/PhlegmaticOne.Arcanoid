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

        private int _currentPercentage;
        private float _previousPercentage;
        private Coroutine _updateRoutine;

        public void SetInNormalizedPercentageAnimate(float percentage)
        {
            if (_updateRoutine != null)
            {
                StopCoroutine(_updateRoutine);
            }

            _currentPercentage = GetPercentage(percentage);
            _updateRoutine = StartCoroutine(UpdatePercentage());
        }
        
        public void SetInNormalizedPercentageInstant(float percentage)
        {
            var calculated = GetPercentage(percentage);
            _percentageText.text = Format(calculated);
            _previousPercentage = calculated;
            _currentPercentage = calculated;
        }
        
        private IEnumerator UpdatePercentage()
        {
            var fps = 1.0f / Time.deltaTime;
            var step = (_currentPercentage - _previousPercentage) / (fps * _updateDuration);
            
            while(_previousPercentage < _currentPercentage)
            {
                _previousPercentage += step;
                
                if (_previousPercentage > _currentPercentage)
                {
                    _previousPercentage = _currentPercentage;
                }

                _percentageText.text = Format((int)_previousPercentage);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
        private static int GetPercentage(float normalized) => (int)(normalized * 100);
        private static string Format(int percentage) => percentage + Percentage;
    }
}