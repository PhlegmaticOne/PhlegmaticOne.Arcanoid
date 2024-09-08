using Libs.Localization.Components.Base;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Libs.Localization.Components
{
    public class ImageLocalizationComponent : LocalizationBindableComponent<Sprite>
    {
        [SerializeField] private Image _image;
        
        protected override void SetLocalizedValue(Sprite value) => _image.sprite = value;
    }
}