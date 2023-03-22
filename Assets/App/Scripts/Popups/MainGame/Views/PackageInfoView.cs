using System.Collections.Generic;
using Common.Packs.Data.Models;
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
        [SerializeField] private bool _appendOneToLevelIndex = true;

        [SerializeField] private bool _hasPackNameView;

        [SerializeField] [ShowIf(nameof(_hasPackNameView))]
        private TextMeshProUGUI _packNameText;
        [SerializeField] [ShowIf(nameof(_hasPackNameView))] 
        private TextMeshProLocalizationComponent _packNameLocalizationComponent;
        
        [SerializeField] private bool _hasColorBindableComponents;
        [SerializeField] [ShowIf(nameof(_hasColorBindableComponents))]
        private List<ColorBindableComponent> _colorBindableComponents;

        private int _levelsCount;
        private int _passedLevelsCount;

        public TextMeshProLocalizationComponent PackNameLocalizationComponent => _packNameLocalizationComponent;

        public void IncreaseLevel()
        {
            _passedLevelsCount++;
            _levelsInfoText.text = FormatLevelsInfo();
        }
        
        public void UpdateLevels(PackPersistentData packPersistentData)
        {
            _passedLevelsCount = packPersistentData.passedLevelsCount;
            _levelsCount = packPersistentData.levelsCount;
            _levelsInfoText.text = FormatLevelsInfo();
        }

        public void SetPackInfo(PackGameData packGameData)
        {
            _levelsCount = packGameData.PackPersistentData.levelsCount;
            _passedLevelsCount = packGameData.PackPersistentData.passedLevelsCount;
            var packConfiguration = packGameData.PackConfiguration;
            _levelsInfoText.text = FormatLevelsInfo();
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

        private string FormatLevelsInfo()
        {
            var passedLevelsCount = _appendOneToLevelIndex ? _passedLevelsCount + 1 : _passedLevelsCount;
            return passedLevelsCount + "/" + _levelsCount;
        }
    }
}