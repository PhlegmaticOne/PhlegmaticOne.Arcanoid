using DG.Tweening;
using Libs.Popups.Animations.Info;
using UnityEngine;

namespace Libs.Popups.Animations.Animators
{
    public class TransformAnimator
    {
        private static readonly Vector3 FullCircle = new Vector3(0, 0, 360);
        
        private readonly Transform _transform;

        public TransformAnimator(Transform transform)
        {
            _transform = transform;
        }

        public Tween FullCircleAnimate(TweenAnimationInfo animationInfo)
        {
            return _transform
                .DORotate(FullCircle, animationInfo.AnimationTime, RotateMode.FastBeyond360)
                .SetUpdate(true)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(animationInfo.Ease);
        }

        public Tween GrowFromZeroX(TweenAnimationInfo animationInfo)
        {
            _transform.localScale = new Vector3(0, 1, 1);
            return _transform.DOScaleX(1f, animationInfo.AnimationTime)
                .SetEase(animationInfo.Ease)
                .SetUpdate(true);
        }
        
        public Tween ScaleToZeroX(TweenAnimationInfo animationInfo)
        {
            return _transform.DOScale(Vector3.zero, animationInfo.AnimationTime)
                .SetEase(animationInfo.Ease)
                .SetUpdate(true);
        }
        
        public Tween GrowFromZero(TweenAnimationInfo animationInfo)
        {
            _transform.localScale = Vector3.zero;
            return _transform.DOScale(Vector3.one, animationInfo.AnimationTime)
                .SetEase(animationInfo.Ease)
                .SetUpdate(true);
        }
    }
}