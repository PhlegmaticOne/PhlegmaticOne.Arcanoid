using System;
using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using DG.Tweening;
using Libs.Localization.Components;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Info;
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
        
        [SerializeField] private bool _hasColorBindableComponent;
        [SerializeField] [ShowIf(nameof(_hasColorBindableComponent))]
        private Image _colorBindableComponent;

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

        public void AppendShowAnimationToSequence(Sequence s, TweenAnimationInfo showAnimationInfo)
        {
            s.Append(Animate.Transform(_packIconImage.transform).GrowFromZero(showAnimationInfo));
            s.Append(Animate.Transform(_packNameText.transform).GrowFromZero(showAnimationInfo));
        }

        public void UpdatePackDataAnimate(PackGameData packGameData, string nextPackName, 
            Sequence s,
            TweenAnimationInfo scaleAnimationInfo,
            TweenAnimationInfo colorAnimationInfo)
        {
            var packConfiguration = packGameData.PackConfiguration;
            var packPersistentData = packGameData.PackPersistentData;
            
            s.Append(Animate.Transform(_packIconImage.transform).ScaleToZeroX(scaleAnimationInfo));
            s.Append(Animate.Transform(_packNameText.transform).ScaleToZeroX(scaleAnimationInfo));
            s.AppendCallback(() =>
            {
                _packIconImage.sprite = packConfiguration.PackImage;
                _packNameLocalizationComponent.SetBindingData<string>(packConfiguration.Name);
                _packNameLocalizationComponent.SetLocalizedValue(nextPackName);
                UpdateLevels(packPersistentData);
            });
            AppendShowAnimationToSequence(s, scaleAnimationInfo);
            s.Append(Animate.Image(_colorBindableComponent).Color(packConfiguration.PackColor, colorAnimationInfo));
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

            if (_hasColorBindableComponent)
            {
                _colorBindableComponent.color = packConfiguration.PackColor;
            }
        }
        
        private string FormatLevelsInfo()
        {
            var passedLevelsCount = _appendOneToLevelIndex ? _passedLevelsCount + 1 : _passedLevelsCount;
            return passedLevelsCount + "/" + _levelsCount;
        }
    }
}