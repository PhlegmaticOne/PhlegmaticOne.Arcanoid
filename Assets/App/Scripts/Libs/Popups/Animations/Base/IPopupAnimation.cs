using UnityEngine.Events;

namespace Libs.Popups.Animations.Base
{
    public interface IPopupAnimation
    {
        void OnAnimationPlayed(UnityAction unityAction);
        void Play(Popup popup, float duration);
        void Stop(Popup popup);
    }
}