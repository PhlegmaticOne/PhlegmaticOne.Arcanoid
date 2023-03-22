using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class DoTweenCallbackAnimation : PopupAnimationBase
    {
        private Func<Tween> _animationCallback;

        private Tween _tween;

        public DoTweenCallbackAnimation(Func<Tween> animationCallback) => _animationCallback = animationCallback;

        public override void Play()
        {
            _tween = _animationCallback.Invoke();   
            _tween.OnComplete(OnAnimationPlayed);
        }

        public override void Stop()
        {
            _animationCallback = null;
            _tween.Kill();
            _tween = null;
        }
    }
}