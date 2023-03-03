using System.Collections.Generic;
using Libs.Localization.Base;
using TMPro;
using UnityEngine;

namespace Libs.Localization
{
    public class LocalizationSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _languagesDropdown;
        private ILocalizationManager _localizationManager;

        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            FillDropdown();
        }

        public void Disable() => _languagesDropdown.enabled = false;

        public void Enable() => _languagesDropdown.enabled = true;

        public void Reset()
        {
            _languagesDropdown.options.Clear();
            _languagesDropdown.onValueChanged.RemoveAllListeners();
        }

        private void FillDropdown()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            var selectedLocaleIndex = 0;
            var i = 0;

            foreach (var availableLocale in _localizationManager.GetAvailableLocales())
            {
                if (_localizationManager.CurrentLocale == availableLocale)
                {
                    selectedLocaleIndex = i;
                }
                options.Add(new TMP_Dropdown.OptionData(availableLocale));

                ++i;
            }

            _languagesDropdown.options = options;
            _languagesDropdown.value = selectedLocaleIndex;
            _languagesDropdown.onValueChanged.AddListener(LocaleSelected);
        }

        private void LocaleSelected(int selectedOption)
        {
            var selected = _languagesDropdown.options[selectedOption].text;
            
            if (_localizationManager.CurrentLocale != selected)
            {
                _localizationManager.SetLocale(selected);
            }
        }
    }
}