using System;
using DG.Tweening;
using Libs.Popups.Animations.Animators;
using Libs.Popups.Animations.Info;
using Libs.Popups.Animations.Info.Types;

namespace Libs.Popups.Animations.Extensions
{
    public static class AnimatorsExtensions
    {
        public static Tween Appear(this RectTransformAnimator rectTransformAnimator,
            AppearTweenAnimationInfo appearTweenAnimationInfo)
        {
            switch (appearTweenAnimationInfo.AppearType)
            {
                case AppearType.FromLeft:
                    return rectTransformAnimator.FromLeft(appearTweenAnimationInfo);
                case AppearType.FromRight:
                    return rectTransformAnimator.FromRight(appearTweenAnimationInfo);
                case AppearType.FromTop:
                    return rectTransformAnimator.FromTop(appearTweenAnimationInfo);
                case AppearType.FromBottom:
                    return rectTransformAnimator.FromBottom(appearTweenAnimationInfo);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static Tween Disappear(this RectTransformAnimator rectTransformAnimator,
            DisappearTweenAnimationInfo disappearTweenAnimationInfo)
        {
            switch (disappearTweenAnimationInfo.DisappearType)
            {
                case DisappearType.ToLeft:
                    return rectTransformAnimator.ToLeft(disappearTweenAnimationInfo);
                case DisappearType.ToRight:
                    return rectTransformAnimator.ToRight(disappearTweenAnimationInfo);
                case DisappearType.ToTop:
                    return rectTransformAnimator.ToTop(disappearTweenAnimationInfo);
                case DisappearType.ToBottom:
                    return rectTransformAnimator.ToBottom(disappearTweenAnimationInfo);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}