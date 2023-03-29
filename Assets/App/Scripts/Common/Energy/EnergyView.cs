using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Common.Energy
{
    public class EnergyView : MonoBehaviour
    {
        private const string TimeFormat = @"mm\:ss";
        
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Image _timeTextPlace;

        private int _maxEnergy;
        private int _currentEnergy;
        public event Action Enabled;

        public void Init(int currentEnergy, int maxEnergy)
        {
            _currentEnergy = currentEnergy;
            _maxEnergy = maxEnergy;
            ChangeEnergyInstant(0);
        }
        
        public void AppendAnimationToSequence(Sequence s, int energyToChange, float time)
        {
            s.AppendCallback(() => ChangeEnergyAnimate(energyToChange, time));
            s.AppendInterval(time);
        }
        
        private void ChangeEnergyAnimate(int energyChanged, float time) => StartCoroutine(UpdateCoroutine(energyChanged, time));

        private IEnumerator UpdateCoroutine(int energyChanged, float time)
        {
            var toAdd = (int)Mathf.Sign(energyChanged);
            var delta = time / (toAdd * energyChanged);
            var currentTime = 0f;

            while (currentTime < time)
            {
                ChangeEnergyInstant(toAdd);
                currentTime += delta;
                yield return new WaitForSecondsRealtime(delta);
            }
        }

        public void ChangeEnergyInstant(int energyChanged)
        {
            _currentEnergy += energyChanged;
            
            if (_currentEnergy < _maxEnergy)
            {
                if (_timeText.text == string.Empty)
                {
                    Enabled?.Invoke();
                }
                _timeTextPlace.gameObject.SetActive(true);
            }
            else
            {
                SetIsFull();
            }

            _energyText.text = FormatEnergy(_currentEnergy, _maxEnergy);
            _slider.value = CalculateValue(_currentEnergy, _maxEnergy);
        }

        public void SetTime(int timeInSeconds) => _timeText.text = FormatTime(timeInSeconds);

        private void SetIsFull()
        {
            _timeText.text = string.Empty;
            _timeTextPlace.gameObject.SetActive(false);
        }

        private static float CalculateValue(int currentEnergy, int maxEnergy)
        {
            var result = (float)currentEnergy / maxEnergy;
            return result >= 1 ? 1 : result;
        }

        private static string FormatEnergy(int currentEnergy, int maxEnergy) => 
            currentEnergy + "/" + maxEnergy;

        private static string FormatTime(int timeInSeconds)
        {
            var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            return timeSpan.ToString(TimeFormat);
        }
    }
}