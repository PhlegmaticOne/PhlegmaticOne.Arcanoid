using System.Collections.Generic;
using Common.Configurations.Packs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame.Views
{
    public class PackageInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelsInfoText;
        [SerializeField] private Image _packIconImage;

        [SerializeField] private bool _hasPackNameView;

        [SerializeField] [ShowIf(nameof(_hasPackNameView))]
        private TextMeshProUGUI _packNameText;
        
        [SerializeField] private bool _hasColorBindableComponents;
        [SerializeField] [ShowIf(nameof(_hasColorBindableComponents))]
        private List<ColorBindableComponent> _colorBindableComponents;

        public void SetPackInfo(PackConfiguration packConfiguration)
        {
            _levelsInfoText.text = FormatLevelsInfo(packConfiguration);
            _packIconImage.sprite = packConfiguration.PackImage;

            if (_hasPackNameView)
            {
                _packNameText.text = packConfiguration.Name;
            }

            if (_hasColorBindableComponents)
            {
                foreach (var colorBindableComponent in _colorBindableComponents)
                {
                    colorBindableComponent.BindColor(packConfiguration.PackColor);
                }
            }
        }
        
        private static string FormatLevelsInfo(PackConfiguration packConfiguration) => 
            (packConfiguration.PassedLevelsCount + 1) + "/" + packConfiguration.LevelsCount;
    }
}