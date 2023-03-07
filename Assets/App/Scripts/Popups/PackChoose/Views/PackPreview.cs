﻿using Common.Configurations.Packs;
using Libs.Localization.Components.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups.PackChoose.Views
{
    public class PackPreview : MonoBehaviour
    {
        [SerializeField] private Image _packSpriteImage;
        [SerializeField] private Image _previewInnerImage;
        [SerializeField] private Image _previewOuterImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelInfoText;

        [SerializeField] private LocalizationBindableComponent _packNameTextBindableComponent;
        public LocalizationBindableComponent PackNameTextBindableComponent => _packNameTextBindableComponent;

        private int _index;

        public event UnityAction<int> Clicked;
        
        public void UpdateView(int index, PackConfiguration packConfiguration)
        {
            _index = index;
            _packSpriteImage.sprite = packConfiguration.PackImage;
            _previewInnerImage.color = packConfiguration.PreviewInnerColor;
            _previewOuterImage.color = packConfiguration.PackColor;
            _packNameText.text = packConfiguration.Name;
            _packNameText.color = packConfiguration.TextColor;
            _levelInfoText.color = packConfiguration.TextColor;
            _levelInfoText.text = FormatLevelsInfo(packConfiguration);
            _packNameTextBindableComponent.SetBindingData<string>(packConfiguration.Name);
        }

        private void OnMouseDown() => Clicked?.Invoke(_index);

        private static string FormatLevelsInfo(PackConfiguration packConfiguration) => 
            packConfiguration.PassedLevelsCount + "/" + packConfiguration.LevelsCount;
    }
}