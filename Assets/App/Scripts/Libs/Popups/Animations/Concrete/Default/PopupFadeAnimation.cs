using DG.Tweening;
using Libs.Popups.Animations.Base;
using UnityEngine;

namespace Libs.Popups.Animations.Concrete.Default
{
    public class PopupFadeAnimation : PopupAnimationBase
    {
        private readonly Popup _popup;
        private readonly float _duration;
        private readonly bool _fadeIn;
        public PopupFadeAnimation(Popup popup, float duration, bool fadeIn)
        {
            _popup = popup;
            _duration = duration;
            _fadeIn = fadeIn;
        }
        
        public override void Play()
        {
            if (_fadeIn)
            {
                _popup.PopupView.Transparent();
            }
            
            var to = _fadeIn ? 1f : 0f;
            var popupTransform = _popup.RectTransform;
            var canvasGroup = _popup.PopupView.CanvasGroup;
            popupTransform.localPosition = Vector3.zero;
            canvasGroup.DOFade(to, _duration).SetUpdate(true).OnComplete(OnAnimationPlayed);
        }

        public override void Stop() => _popup.PopupView.CanvasGroup.DOKill();
    }
}