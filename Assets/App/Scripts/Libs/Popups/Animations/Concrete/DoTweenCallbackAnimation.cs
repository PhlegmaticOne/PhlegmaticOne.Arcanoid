using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class DoTweenCallbackAnimation : PopupAnimationBase
    {
        private readonly Func<Tween> _animationCallback;
        private readonly Action _onStopAction;

        public DoTweenCallbackAnimation(Func<Tween> animationCallback, Action onStopAction)
        {
            _animationCallback = animationCallback;
            _onStopAction = onStopAction;
        }
        
        public override void Play(Popup popup, float duration) => _animationCallback().OnComplete(OnAnimationPlayed);
        public override void Stop(Popup popup) => _onStopAction?.Invoke();
    }
}