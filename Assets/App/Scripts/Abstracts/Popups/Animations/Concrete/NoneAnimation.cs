using UnityEngine;

namespace Abstracts.Popups.Animations.Concrete
{
    public class NoneAnimation : PopupAnimationBase
    {
        public override void Play(Popup popup, float duration) => popup.RectTransform.localPosition = Vector3.zero;
        public override void Stop(Popup popup) { }
    }
}