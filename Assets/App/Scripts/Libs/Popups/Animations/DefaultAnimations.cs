using DG.Tweening;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete.Default;
using UnityEngine;

namespace Libs.Popups.Animations
{
    public static class DefaultAnimations
    {
        public static IPopupAnimation None() => new NoneAnimation();

        public static Tween FromTop(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var initial = transform.localPosition;
            PositionsHelper.ToTop(transform, parent);
            return transform.DOLocalMove(initial, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween FromBottom(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var initial = transform.localPosition;
            PositionsHelper.ToBottom(transform, parent);
            return transform.DOLocalMove(initial, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween FromRight(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var initial = transform.localPosition;
            PositionsHelper.ToRight(transform, parent);
            return transform.DOLocalMove(initial, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween FromLeft(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var initial = transform.localPosition;
            PositionsHelper.ToLeft(transform, parent);
            return transform.DOLocalMove(initial, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween FadeIn(CanvasGroup canvasGroup, TweenAnimationInfo animationInfo)
        {
            canvasGroup.alpha = 0;
            return canvasGroup.DOFade(1f, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);;
        }
        
        public static Tween FadeOut(CanvasGroup canvasGroup, TweenAnimationInfo animationInfo)
        {
            return canvasGroup.DOFade(0f, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);;
        }
        
        public static Tween ToRight(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Right(parent);
            to.y = transform.localPosition.y;
            return transform.DOLocalMove(to, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween ToLeft(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Left(parent);
            to.y = transform.localPosition.y;
            return transform.DOLocalMove(to, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween ToTop(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Top(parent);
            to.x = transform.localPosition.x;
            return transform.DOLocalMove(to, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
        
        public static Tween ToBottom(RectTransform transform, RectTransform parent, TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Bottom(parent);
            to.x = transform.localPosition.x;
            return transform.DOLocalMove(to, animationInfo.AnimationTime).SetUpdate(true).SetEase(animationInfo.Ease);
        }
    }
}