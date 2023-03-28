using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Localization.Base;
using Libs.Localization.Components;
using Libs.Localization.Models;
using Libs.Popups.Controls.Base;
using TMPro;
using UnityEngine;

namespace Popups.Settings.Controls
{
    public class LocalizationDropdownControl : ControlBase
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private ImageLocalizationComponent _imageLocalizationComponent;

        private ILocalizationManager _localizationManager;
        public override void OnClick(Action<ControlBase> action)
        {
            _dropdown.onValueChanged.AddListener(i =>
            {
                ControlValue = _dropdown.options[i].text;
                action(this);
            });
        }

        public void Initialize(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            _localizationManager.LocaleChanged += LocalizationManagerOnLocaleChanged;
            FillDropdown();
        }

        private void LocalizationManagerOnLocaleChanged(LocaleInfo obj)
        {
            RefillDropdown();
        }

        private void FillDropdown()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            var selectedLocaleIndex = 0;
            var i = 0;

            var availableLocales = _localizationManager.GetAvailableLocales().ToList();
            
            foreach (var availableLocale in availableLocales)
            {
                if (_localizationManager.CurrentLocale.DisplayName == availableLocale.DisplayName)
                {
                    selectedLocaleIndex = i;
                }
                options.Add(new TMP_Dropdown.OptionData(availableLocale.DisplayName));

                ++i;
            }

            _dropdown.options = options;
            _dropdown.value = selectedLocaleIndex;
            UpdateElements();
        }

        private void RefillDropdown()
        {
            var availableLocales = _localizationManager.GetAvailableLocales().ToList();

            var options = _dropdown.options;
            
            for (var i = 0; i < availableLocales.Count; i++)
            {
                options[i].text = availableLocales[i].DisplayName;
            }

            UpdateElements();
        }

        private void UpdateElements()
        {
            _label.text = _localizationManager.CurrentLocale.DisplayName;
            _imageLocalizationComponent.SetLocalizedValue(_localizationManager
                .GetLocalizedValue<Sprite>(_imageLocalizationComponent.BindingKey));
        }

        public override void Enable() => _dropdown.enabled = true;

        public override void Disable() => _dropdown.enabled = false;

        protected override void ResetProtected()
        {
            _localizationManager.LocaleChanged -= LocalizationManagerOnLocaleChanged;
            _dropdown.onValueChanged.RemoveAllListeners();
        }
    }
}