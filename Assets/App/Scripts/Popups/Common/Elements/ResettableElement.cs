using System.Collections.Generic;
using UnityEngine;

namespace Popups.Common.Elements
{
    public class ResettableElement : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private Vector3 _awakePosition;
        public RectTransform RectTransform => _rectTransform;
        private void Awake() => _awakePosition = _rectTransform.localPosition;
        public void Reset() => _rectTransform.localPosition = _awakePosition;
        
        public static void ResetAll(IEnumerable<ResettableElement> resettableElements)
        {
            foreach (var resettableElement in resettableElements)
            {
                resettableElement.Reset();
            }
        }
    }
}