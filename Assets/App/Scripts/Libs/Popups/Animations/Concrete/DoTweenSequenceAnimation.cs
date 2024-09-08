using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class DoTweenSequenceAnimation : PopupAnimationBase
    {
        private Action<Sequence> _sequenceBuildAction;
        private Action _killAction;
        private Sequence _sequence;
        public DoTweenSequenceAnimation(Action<Sequence> sequenceBuildAction, Action killAction = null)
        {
            _sequenceBuildAction = sequenceBuildAction;
            _killAction = killAction;
        }

        public override void Play()
        {
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(true);
            _sequenceBuildAction.Invoke(_sequence);
            _sequence.Play().OnComplete(OnAnimationPlayed);
        }

        public override void Stop()
        {
            _killAction?.Invoke();
            _sequence?.Kill();
            _killAction = null;
            _sequenceBuildAction = null;
            _sequence = null;
        }
    }
}