using System;
using System.Collections;
using DG.Tweening;
using Libs.Popups.Animations;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Common.Energy
{
    public class EnergyView : MonoBehaviour
    {
        private const string TimeFormat = @"mm\:ss";
        
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private TextMeshProUGUI _timeText;

        [SerializeField] private float _animationTime;

        private int _maxEnergy;
        private int _currentEnergy;

        public void Init(int currentEnergy, int maxEnergy)
        {
            _currentEnergy = currentEnergy;
            _maxEnergy = maxEnergy;
            ChangeEnergyInstant(0);
        }
        
        public void ChangeEnergyAnimate(int energyChanged, float time)
        {
            StartCoroutine(UpdateCoroutine(energyChanged, time));
        }

        public void AppendAnimationToSequence(Sequence s, int energyToChange, float time)
        {
            s.AppendCallback(() => ChangeEnergyAnimate(energyToChange, time));
            s.AppendInterval(time);
        }

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
            _energyText.text = FormatEnergy(_currentEnergy, _maxEnergy);
            _slider.value = CalculateValue(_currentEnergy, _maxEnergy);
        }

        public void SetTime(int timeInSeconds) => _timeText.text = FormatTime(timeInSeconds);
        
        public void SetIsFull() => _timeText.text = string.Empty;

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