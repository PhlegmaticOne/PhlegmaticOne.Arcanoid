using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Localization.Models;
using TMPro;
using UnityEngine;

namespace Libs.Localization
{
    public class LocalizationSelector : MonoBehaviour, ILocalizable
    {
        [SerializeField] private TMP_Dropdown _languagesDropdown;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;
        
        private ILocalizationManager _localizationManager;
        private List<LocaleInfo> _availableLocales;
        private LocalizationContext _localizationContext;

        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            _localizationContext = LocalizationContext.Create(_localizationManager);
            _localizationContext.BindLocalizable(this);
            _localizationContext.Refresh();
            _localizationManager.LocaleChanged += LocalizationManagerOnLocaleChanged;
            FillDropdown();
        }
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        public void Disable() => _languagesDropdown.enabled = false;

        public void Enable() => _languagesDropdown.enabled = true;

        public void Reset()
        {
            _localizationContext.Flush();
            _localizationManager.LocaleChanged -= LocalizationManagerOnLocaleChanged;
            _languagesDropdown.options.Clear();
            _languagesDropdown.onValueChanged.RemoveAllListeners();
        }

        private void LocalizationManagerOnLocaleChanged(LocaleInfo obj) => RefillDropdown();
        
        private void FillDropdown()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            var selectedLocaleIndex = 0;
            var i = 0;

            _availableLocales = _localizationManager.GetAvailableLocales().ToList();
            foreach (var availableLocale in _availableLocales)
            {
                if (_localizationManager.CurrentLocale.DisplayName == availableLocale.DisplayName)
                {
                    selectedLocaleIndex = i;
                }
                options.Add(new TMP_Dropdown.OptionData(availableLocale.DisplayName));

                ++i;
            }

            _languagesDropdown.options = options;
            _languagesDropdown.value = selectedLocaleIndex;
            _languagesDropdown.onValueChanged.AddListener(LocaleSelected);
        }

        private void RefillDropdown()
        {
            _availableLocales = _localizationManager.GetAvailableLocales().ToList();

            var options = _languagesDropdown.options;
            
            for (var i = 0; i < _availableLocales.Count; i++)
            {
                options[i].text = _availableLocales[i].DisplayName;
            }

            _label.text = _localizationManager.CurrentLocale.DisplayName;
        }

        private void LocaleSelected(int selectedOption)
        {
            var selected = _languagesDropdown.options[selectedOption].text;
            
            if (_localizationManager.CurrentLocale.DisplayName != selected)
            {
                _localizationManager.SetLocale(_availableLocales[selectedOption]);
            }
        }

    }
}