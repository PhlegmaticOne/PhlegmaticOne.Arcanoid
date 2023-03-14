using Common.Configurations.Packs;
using Common.Data.Models;
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
        [SerializeField] private Image _previewOuterImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelInfoText;

        [SerializeField] private LocalizationBindableComponent _packNameTextBindableComponent;
        public LocalizationBindableComponent PackNameTextBindableComponent => _packNameTextBindableComponent;

        private int _index;

        public event UnityAction<int> Clicked;
        
        public void UpdateView(int index, PackGameData packGameData)
        {
            _index = index;
            var packConfiguration = packGameData.PackConfiguration;
            _packSpriteImage.sprite = packConfiguration.PackImage;
            _previewOuterImage.color = packConfiguration.PackColor;
            _packNameText.text = packConfiguration.Name;
            _levelInfoText.text = FormatLevelsInfo(packGameData.PackPersistentData);
            _packNameTextBindableComponent.SetBindingData<string>(packConfiguration.Name);
        }

        private void OnMouseDown() => Clicked?.Invoke(_index);

        private static string FormatLevelsInfo(PackPersistentData packPersistentData) => 
            packPersistentData.passedLevelsCount + "/" + packPersistentData.levelsCount;
    }
}