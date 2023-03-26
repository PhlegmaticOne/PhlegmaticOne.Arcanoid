using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using Libs.Localization.Components.Base;
using Libs.Popups.Controls;
using Popups.PackChoose.Views.Configurations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PackChoose.Views
{
    public class PackPreview : ButtonControl
    {
        [SerializeField] private Image _innerImage;
        [SerializeField] private Image _outerImage;
        [SerializeField] private Image _glowImage;
        [SerializeField] private Image _packIconImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelInfoText;
        [SerializeField] private EnergyCountView _startLevelEnergy;
        [SerializeField] private EnergyCountView _winLevelEnergy;

        [SerializeField] private LocalizationBindableComponent _packNameTextBindableComponent;
        public LocalizationBindableComponent PackNameTextBindableComponent => _packNameTextBindableComponent;


        public void ApplyPackGameData(PackGameData packGameData)
        {
            var packConfiguration = packGameData.PackConfiguration;
            SetPackIconSprite(packConfiguration.PackImage);
            UpdateLevelsInfo(packGameData.PackPersistentData);
            SetLocalizationKey(packConfiguration.Name);
            SetEnergyInfo(packGameData.PackConfiguration);
            _glowImage.color = packConfiguration.PackColor;
            _outerImage.color = packConfiguration.PackColor;
            _packNameText.text = packConfiguration.Name;
        }

        public void SetEnergyInfo(PackConfiguration packConfiguration)
        {
            _startLevelEnergy.SetEnergy(packConfiguration.StartLevelEnergy);
            _winLevelEnergy.SetEnergy(packConfiguration.WinLevelEnergy);
        }

        public void SetLocalizationKey(string localizationKey)
        {
            _packNameTextBindableComponent.SetBindingData<string>(localizationKey);
        }

        public void ApplyPackPreviewConfiguration(PackPreviewConfiguration packPreviewConfiguration)
        {
            _glowImage.color = packPreviewConfiguration.GlowColor;
            _outerImage.color = packPreviewConfiguration.OuterColor;
            _innerImage.color = packPreviewConfiguration.InnerColor;
            _levelInfoText.color = packPreviewConfiguration.LevelTextColor;
            _packNameText.color = packPreviewConfiguration.MainTextColor;
        }

        public void SetPackIconSprite(Sprite sprite) => _packIconImage.sprite = sprite;

        public void UpdateLevelsInfo(PackPersistentData packPersistentData)
        {
            _levelInfoText.text = FormatLevelsInfo(packPersistentData);
        }

        public void HideEnergyInfo()
        {
            _startLevelEnergy.Hide();
            _winLevelEnergy.Hide();
        }

        protected override void OnInteractableSet(bool isInteractable) => SetInteractableDirect(true);

        private static string FormatLevelsInfo(PackPersistentData packPersistentData) => 
            packPersistentData.passedLevelsCount + "/" + packPersistentData.levelsCount;
    }
}