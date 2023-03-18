using UnityEngine;

namespace Popups.MainGame.Helpers
{
    public class MainGameMenuHeaderResizer : MonoBehaviour
    {
        [SerializeField] private RectTransform _headerTransform;
        [SerializeField] private RectTransform _leftButtonTransform;
        [SerializeField] private RectTransform _rightButtonTransform;
        [SerializeField] private RectTransform _centerTransform;

        [SerializeField] [Range(0f, 1f)] private float _marginSide;

        private void Start()
        {
            var buttonsWidth = ((1 - 2 * _marginSide) * _headerTransform.rect.width - _centerTransform.rect.width) / 2;
            _leftButtonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonsWidth);
            _rightButtonTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonsWidth);
        }
    }
}