using DG.Tweening;
using Libs.Popups.Animations.Helpers;
using Libs.Popups.Animations.Info;
using UnityEngine;

namespace Libs.Popups.Animations.Animators
{
    public interface IRectTransformAnimator
    {
        RectTransformAnimator RelativeTo(RectTransform rectTransform);
    }
    
    public class RectTransformAnimator : IRectTransformAnimator
    {
        private readonly RectTransform _animated;
        private RectTransform _relativeTo;

        public RectTransformAnimator(RectTransform animated)
        {
            _animated = animated;
        }
        
        public RectTransformAnimator RelativeTo(RectTransform rectTransform)
        {
            _relativeTo = rectTransform;
            return this;
        }

        public Tween FromLeft(TweenAnimationInfo animationInfo)
        {
            var initial = _animated.localPosition;
            PositionsHelper.ToLeft(_animated, _relativeTo);
            return AnimateTo(initial, animationInfo);
        }
        
        public Tween FromTop(TweenAnimationInfo animationInfo)
        {
            var initial = _animated.localPosition;
            PositionsHelper.ToTop(_animated, _relativeTo);
            return AnimateTo(initial, animationInfo);
        }
        
        public Tween FromBottom(TweenAnimationInfo animationInfo)
        {
            var initial = _animated.localPosition;
            PositionsHelper.ToBottom(_animated, _relativeTo);
            return AnimateTo(initial, animationInfo);
        }
        
        public Tween FromRight(TweenAnimationInfo animationInfo)
        {
            var initial = _animated.localPosition;
            PositionsHelper.ToRight(_animated, _relativeTo);
            return AnimateTo(initial, animationInfo);
        }
        
        public Tween ToRight(TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Right(_relativeTo);
            to.y = _animated.localPosition.y;
            return AnimateTo(to, animationInfo);
        }
        
        public Tween ToLeft(TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Left(_relativeTo);
            to.y = _animated.localPosition.y;
            return AnimateTo(to, animationInfo);
        }
        
        public Tween ToTop(TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Top(_relativeTo);
            to.x = _animated.localPosition.x;
            return AnimateTo(to, animationInfo);
        }
        
        public Tween ToBottom(TweenAnimationInfo animationInfo)
        {
            var to = PositionsHelper.Bottom(_relativeTo);
            to.x = _animated.localPosition.x;
            return AnimateTo(to, animationInfo);
        }

        private Tween AnimateTo(in Vector3 position, TweenAnimationInfo animationInfo)
        {
            return _animated
                .DOLocalMove(position, animationInfo.AnimationTime)
                .SetUpdate(true)
                .SetEase(animationInfo.Ease);
        }
    }
}