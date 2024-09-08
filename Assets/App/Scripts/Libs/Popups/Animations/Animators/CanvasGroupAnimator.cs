using DG.Tweening;
using Libs.Popups.Animations.Info;
using UnityEngine;

namespace Libs.Popups.Animations.Animators
{
    public class CanvasGroupAnimator
    {
        private readonly CanvasGroup _canvasGroup;

        public CanvasGroupAnimator(CanvasGroup canvasGroup) => _canvasGroup = canvasGroup;

        public Tween FadeIn(TweenAnimationInfo animationInfo)
        {
            _canvasGroup.alpha = 0;
            return AnimateTo(1f, animationInfo);
        }

        public Tween FadeOut(TweenAnimationInfo animationInfo) => AnimateTo(0f, animationInfo);

        private Tween AnimateTo(float endValue, TweenAnimationInfo animationInfo) => 
            _canvasGroup.DOFade(endValue, animationInfo.AnimationTime)
                .SetUpdate(true)
                .SetEase(animationInfo.Ease);
    }
}