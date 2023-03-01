using System.Collections.Generic;
using Abstracts.Popups.Animations.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Abstracts.Popups.Animations.Concrete
{
    public abstract class PopupAnimationBase : IPopupAnimation
    {
        private readonly List<UnityAction> _onAnimationPlayed;

        protected PopupAnimationBase() => _onAnimationPlayed = new List<UnityAction>();

        public void OnAnimationPlayed(UnityAction unityAction) => _onAnimationPlayed.Add(unityAction);

        public abstract void Play(Popup popup, float duration);

        public abstract void Stop(Popup popup);

        protected void ExecuteAllActions()
        {
            foreach (var action in _onAnimationPlayed)
            {
                action?.Invoke();
            }
        }
    }
}