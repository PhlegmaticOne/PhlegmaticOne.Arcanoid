using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Abstracts.Popups.Animations.Concrete
{
    public class PopupMoveAnimation : PopupAnimationBase
    {
        private readonly Vector3 _from;
        private readonly Vector3 _to;

        public PopupMoveAnimation(Vector3 from, Vector3 to)
        {
            _from = from;
            _to = to;
        }

        public static PopupMoveAnimation ToZeroFrom(Vector3 from) =>
            new PopupMoveAnimation(from, Vector3.zero);

        public static PopupMoveAnimation FromZeroTo(Vector3 to) =>
            new PopupMoveAnimation(Vector3.zero, to);
        
        public override void Play(Popup popup, float duration)
        {
            var popupTransform = popup.RectTransform;
            popupTransform.localPosition = _from;
            popupTransform.DOLocalMove(_to, duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(ExecuteAllActions);
        }

        public override void Stop(Popup popup)
        {
            popup.RectTransform.DOKill();
        }
    }
}