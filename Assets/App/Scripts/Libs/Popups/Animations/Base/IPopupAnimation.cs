using System;

namespace Libs.Popups.Animations.Base
{
    public interface IPopupAnimation
    {
        event Action AnimationPlayed;
        void Play();
        void Stop();
    }
}