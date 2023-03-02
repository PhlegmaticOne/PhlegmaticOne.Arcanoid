using Scenes.MainGameScene.Configurations.Packs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scenes.ChoosePackPopup.Views
{
    public class PackPreview : MonoBehaviour
    {
        [SerializeField] private Image _packSpriteImage;
        [SerializeField] private Image _previewInnerImage;
        [SerializeField] private Image _previewOuterImage;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _levelInfoText;

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
        }

        private void OnMouseDown() => Clicked?.Invoke(_index);

        private static string FormatLevelsInfo(PackConfiguration packConfiguration) => 
            packConfiguration.PassedLevelsCount + "/" + packConfiguration.LevelsCount;
    }
}