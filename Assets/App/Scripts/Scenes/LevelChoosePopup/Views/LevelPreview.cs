using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scenes.LevelChoosePopup.Views
{
    public class LevelPreview : MonoBehaviour
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private int _index;
        public event UnityAction<int> LevelClicked;

        public void UpdateView(int number, Color backColor)
        {
            _index = number;
            _levelButton.image.color = backColor;
            _levelButton.onClick.AddListener(OnLevelClicked);
            _buttonText.text = number.ToString();
        }

        public void Disable() => _levelButton.enabled = false;
        public void Enable() => _levelButton.enabled = true;

        private void OnDisable() => _levelButton.onClick.RemoveAllListeners();
        private void OnLevelClicked() => LevelClicked?.Invoke(_index);
    }
}