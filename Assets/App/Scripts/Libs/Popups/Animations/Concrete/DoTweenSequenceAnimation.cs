using System;
using DG.Tweening;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class DoTweenSequenceAnimation : PopupAnimationBase
    {
        private readonly Action<Sequence> _sequenceBuildAction;
        private Sequence _sequence;
        public DoTweenSequenceAnimation(Action<Sequence> sequenceBuildAction) => 
            _sequenceBuildAction = sequenceBuildAction;

        public override void Play()
        {
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(true);
            _sequenceBuildAction.Invoke(_sequence);
            _sequence.Play().OnComplete(OnAnimationPlayed);
        }

        public override void Stop() => _sequence.Kill();
    }
}