using System;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Base
{
    public abstract class PopupAnimationBase : IPopupAnimation
    {
        public event Action AnimationPlayed;
        
        public abstract void Play(Popup popup, float duration);

        public abstract void Stop(Popup popup);

        protected void OnAnimationPlayed() => AnimationPlayed?.Invoke();
    }
}