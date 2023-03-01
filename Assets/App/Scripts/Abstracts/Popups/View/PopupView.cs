using UnityEngine;

namespace Abstracts.Popups.View
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Canvas _canvas;
        public CanvasGroup CanvasGroup => _canvasGroup;

        public void SetSortOrder(int sortOrder)
        {
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = sortOrder;
        }

        public void Transparent() => _canvasGroup.alpha = 0;
    }
}