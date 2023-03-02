using UnityEngine;

namespace Common.Positioning
{
    public class ViewportResizer : MonoBehaviour
    {
        [SerializeField] private RectTransform _viewPortTransform;
        [SerializeField] private RectTransform _canvasTransform;
        [SerializeField] private RectTransform _headerTransform;

        private void Start()
        {
            var height = _canvasTransform.rect.height - _headerTransform.rect.height;
            _viewPortTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            _viewPortTransform.anchorMin = new Vector2(0, 0);
            _viewPortTransform.anchorMax = new Vector2(1, 0);
        }
    }
}