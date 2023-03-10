using TMPro;
using UnityEngine;

namespace Popups.MainGame.Views
{
    public class LevelPassPercentageView : MonoBehaviour
    {
        private const string Percentage = "%";
        [SerializeField] private TextMeshProUGUI _percentageText;

        public void SetInNormalizedPercentage(float percentage) => 
            _percentageText.text = GetPercentage(percentage) + Percentage;

        private static string GetPercentage(float normalized) => ((int)(normalized * 100)).ToString();
    }
}