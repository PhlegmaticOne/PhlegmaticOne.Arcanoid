using System;
using DG.Tweening;
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

        public void SetEnergyAnimate(int currentEnergy, int maxEnergy, Action onAnimationPlayed = null)
        {
            _energyText.text = FormatEnergy(currentEnergy, maxEnergy);
            _slider.DOValue(CalculateValue(currentEnergy, maxEnergy), _animationTime)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    onAnimationPlayed?.Invoke();
                    _slider.DOKill();
                });
        }

        public void SetEnergyInstant(int currentEnergy, int maxEnergy)
        {
            _energyText.text = FormatEnergy(currentEnergy, maxEnergy);
            _slider.value = CalculateValue(currentEnergy, maxEnergy);
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