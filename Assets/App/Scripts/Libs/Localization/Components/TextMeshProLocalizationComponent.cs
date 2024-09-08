using Libs.Localization.Components.Base;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Libs.Localization.Components
{
    public class TextMeshProLocalizationComponent : LocalizationBindableComponent<string>
    {
        [Required] [SerializeField] private TextMeshProUGUI _textMeshPro;
        
        protected override void SetLocalizedValue(string value) => _textMeshPro.text = value;
    }
}