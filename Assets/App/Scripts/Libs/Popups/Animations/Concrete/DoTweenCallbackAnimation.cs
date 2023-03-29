using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class DoTweenCallbackAnimation : PopupAnimationBase
    {
        private Func<Tween> _animationCallback;
        private Action _killAction;
        private Tween _tween;

        public DoTweenCallbackAnimation(Func<Tween> animationCallback, Action killAction = null)
        {
            _animationCallback = animationCallback;
            _killAction = killAction;
        }

        public override void Play()
        {
            _tween = _animationCallback.Invoke();
            _tween.Play().OnComplete(OnAnimationPlayed);
        }

        public override void Stop()
        {
            _killAction?.Invoke();
            _tween?.Kill();
            _tween = null;
            _animationCallback = null;
            _killAction = null;
        }
    }
}