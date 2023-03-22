using DG.Tweening;
using Libs.Popups.Animations.Info;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.Popups.Animations.Animators
{
    public class ImageAnimator
    {
        private readonly Image _image;

        public ImageAnimator(Image image)
        {
            _image = image;
        }

        public Tween Color(Color final, TweenAnimationInfo tweenAnimationInfo)
        {
            return _image.DOColor(final, tweenAnimationInfo.AnimationTime)
                .SetEase(tweenAnimationInfo.Ease)
                .SetUpdate(true);
        }
    }
}