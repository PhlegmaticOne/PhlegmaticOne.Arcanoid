using Scenes.MainGameScene.Configurations.Packs;
using TMPro;
using UnityEngine;
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

        public void UpdateView(PackConfiguration packConfiguration)
        {
            _packSpriteImage.sprite = packConfiguration.PackImage;
            _previewInnerImage.color = packConfiguration.PackColor;
            _previewOuterImage.color = packConfiguration.PackColor;
            _packNameText.text = packConfiguration.Name;
            _levelInfoText.text = FormatLevelsInfo(packConfiguration);
        }

        private static string FormatLevelsInfo(PackConfiguration packConfiguration) => 
            packConfiguration.PassedLevelsCount + "/" + packConfiguration.LevelsCount;
    }
}