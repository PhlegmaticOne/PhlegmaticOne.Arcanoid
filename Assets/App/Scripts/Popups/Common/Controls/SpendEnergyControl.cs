using Libs.Popups.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Common.Controls
{
    public class SpendEnergyControl : ButtonControl
    {
        [SerializeField] private Image _disabledImage;
        [SerializeField] private TextMeshProUGUI _energyText;

        protected override void OnInteractableSet(bool isInteractable)
        {
            if (isInteractable == false)
            {
                _disabledImage.gameObject.SetActive(true);
                Disable();
            }
            else
            {
                _disabledImage.gameObject.SetActive(false);
                Enable();
            }
        }

        public void SetEnergy(int energy)
        {
            _energyText.text = (-energy).ToString();
        }
    }
}