using DG.Tweening;
using UnityEngine;

namespace Abstracts.Popups.Animations.Concrete
{
    public class PopupFadeAnimation : PopupAnimationBase
    {
        private readonly bool _fadeIn;
        public PopupFadeAnimation(bool fadeIn) => _fadeIn = fadeIn;
        public override void Play(Popup popup, float duration)
        {
            if (_fadeIn)
            {
                popup.PopupView.Transparent();
            }
            
            var to = _fadeIn ? 1f : 0f;
            var popupTransform = popup.RectTransform;
            var canvasGroup = popup.PopupView.CanvasGroup;
            popupTransform.localPosition = Vector3.zero;
            canvasGroup.DOFade(to, duration).SetUpdate(true).OnComplete(ExecuteAllActions);
        }

        public override void Stop(Popup popup) => popup.PopupView.CanvasGroup.DOKill();
    }
}