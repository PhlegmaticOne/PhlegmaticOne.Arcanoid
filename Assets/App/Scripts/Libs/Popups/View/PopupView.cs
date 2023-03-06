using UnityEngine;

namespace Libs.Popups.View
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Canvas _canvas;
        public CanvasGroup CanvasGroup => _canvasGroup;

        public void SetSortOrder(int sortOrder)
        {
            OverrideSorting();
            _canvas.sortingOrder = sortOrder;
        }

        public void SetSortingLayer(string sortingLayerName)
        {
            OverrideSorting();
            _canvas.sortingLayerName = sortingLayerName;
        }

        public void Transparent() => _canvasGroup.alpha = 0;

        private void OverrideSorting()
        {
            if (_canvas.overrideSorting == false)
            {
                _canvas.overrideSorting = true;
            }
        }
    }
}