using System;

namespace Libs.Popups.Animations.Base
{
    public interface IPopupAnimation
    {
        event Action AnimationPlayed;
        void Play(Popup popup, float duration);
        void Stop(Popup popup);
    }
}