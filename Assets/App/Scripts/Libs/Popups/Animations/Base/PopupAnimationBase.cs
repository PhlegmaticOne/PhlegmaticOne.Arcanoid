using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Base
{
    public abstract class PopupAnimationBase : IPopupAnimation
    {
        public event Action AnimationPlayed;
        
        public abstract void Play();

        public abstract void Stop();

        protected void OnAnimationPlayed() => AnimationPlayed?.Invoke();
    }
}