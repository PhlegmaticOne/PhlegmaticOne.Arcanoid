using DG.Tweening;
using Libs.Popups.Animations.Base;
using UnityEngine;

namespace Libs.Popups.Animations.Concrete.Default
{
    public class PopupMoveAnimation : PopupAnimationBase
    {
        private readonly RectTransform _rectTransform;
        private readonly float _duration;
        private readonly Vector3 _from;
        private readonly Vector3 _to;

        public PopupMoveAnimation(RectTransform transform, float duration, Vector3 from, Vector3 to)
        {
            _rectTransform = transform;
            _duration = duration;
            _from = from;
            _to = to;
        }

        public static PopupMoveAnimation ToZeroFrom(RectTransform transform, float duration, Vector3 from) =>
            new PopupMoveAnimation(transform, duration, from, Vector3.zero);

        public static PopupMoveAnimation FromZeroTo(RectTransform transform, float duration, Vector3 to) =>
            new PopupMoveAnimation(transform, duration, Vector3.zero, to);
        
        public override void Play()
        {
            _rectTransform.localPosition = _from;
            _rectTransform.DOLocalMove(_to, _duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(OnAnimationPlayed);
        }

        public override void Stop() => _rectTransform.DOKill();
    }
}