using Libs.Popups.Animations.Base;
using UnityEngine;

namespace Libs.Popups.Animations.Concrete.Default
{
    public class NoneAnimation : PopupAnimationBase
    {
        public override void Play(Popup popup, float duration)
        {
            popup.RectTransform.localPosition = Vector3.zero;
            OnAnimationPlayed();
        }

        public override void Stop(Popup popup) { }
    }
}