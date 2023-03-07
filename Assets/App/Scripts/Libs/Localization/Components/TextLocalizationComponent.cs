using Libs.Localization.Components.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.Localization.Components
{
    public class TextLocalizationComponent : LocalizationBindableComponent<string>
    {
        [SerializeField] private Text _text;
        
        protected override void SetLocalizedValue(string value) => _text.text = value;
    }
}