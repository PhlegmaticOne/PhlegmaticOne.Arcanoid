using System.Collections.Generic;
using Common.Configurations.Packs;
using Common.Data.Models;
using Libs.Localization.Components;
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
        [SerializeField] [ShowIf(nameof(_hasPackNameView))] 
        private TextMeshProLocalizationComponent _packNameLocalizationComponent;
        
        [SerializeField] private bool _hasColorBindableComponents;
        [SerializeField] [ShowIf(nameof(_hasColorBindableComponents))]
        private List<ColorBindableComponent> _colorBindableComponents;

        public TextMeshProLocalizationComponent PackNameLocalizationComponent => _packNameLocalizationComponent;

        public void SetPackInfo(PackGameData packGameData)
        {
            var packConfiguration = packGameData.PackConfiguration;
            _levelsInfoText.text = FormatLevelsInfo(packGameData.PackPersistentData);
            _packIconImage.sprite = packConfiguration.PackImage;
            
            if (_hasPackNameView)
            {
                _packNameText.text = packConfiguration.Name;
                _packNameLocalizationComponent.SetBindingData<string>(packConfiguration.Name);
            }

            if (_hasColorBindableComponents)
            {
                foreach (var colorBindableComponent in _colorBindableComponents)
                {
                    colorBindableComponent.BindColor(packConfiguration.PackColor);
                }
            }
        }

        private static string FormatLevelsInfo(PackPersistentData packPersistentData) => 
            packPersistentData.passedLevelsCount + 1 + "/" + packPersistentData.levelsCount;
    }
}