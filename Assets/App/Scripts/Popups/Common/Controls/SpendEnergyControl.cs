using Libs.Popups.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Common.Controls
{
    public class SpendEnergyControl : ButtonControl
    {
        [SerializeField] private Image _disabledImage;
        [SerializeField] private GameObject _energyTextRoot;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private RectTransform _mainTextTransform;

        private Vector2 _offset;
        
        protected override void Initialize() => _offset = _mainTextTransform.offsetMax;

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

        public void ChangeEnergyViewEnabled(bool isEnabled)
        {
            _energyTextRoot.SetActive(isEnabled);
            _mainTextTransform.offsetMax = isEnabled == false ? new Vector2(0, _offset.y) : _offset;
        }

        public void SetEnergy(int energy) => _energyText.text = energy.ToString();
    }
}