using Abstracts.Pooling.Base;
using Abstracts.Popups.Animations.Base;
using Abstracts.Popups.Configurations;
using Abstracts.Popups.View;
using UnityEngine;
using UnityEngine.UI;

namespace Abstracts.Popups
{
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        [SerializeField] private PopupView _popupView;
        [SerializeField] private PopupConfiguration _popupConfiguration;

        public PopupView PopupView => _popupView;
        public RectTransform RectTransform => transform as RectTransform;
        public PopupConfiguration PopupConfiguration => _popupConfiguration;
        
        public abstract void EnableInput();
        
        public abstract void DisableInput();
        
        public void Show(IPopupAnimation popupAnimation, int sortingOrder)
        {
            _popupView.SetSortOrder(sortingOrder);
            OnBeforeShowing();
            popupAnimation.OnAnimationPlayed(OnShow);
            popupAnimation.Play(this, PopupConfiguration.AppearanceTime);
        }
        
        public void Hide(IPopupAnimation popupAnimation)
        {
            OnBeforeHiding();
            popupAnimation.OnAnimationPlayed(OnHid);
            popupAnimation.Play(this, PopupConfiguration.DisappearanceTime);
        }
        
        protected virtual void OnBeforeShowing() { }
        protected virtual void OnBeforeHiding() { }
        protected virtual void OnShow() => EnableInput();
        protected virtual void OnHid() => DisableInput();
        
        public virtual void Reset() { }
        
        protected static void DisableBehaviour(Behaviour behaviour) => behaviour.enabled = false;
        protected static void EnableBehaviour(Behaviour behaviour) => behaviour.enabled = true;
        protected static void RemoveAllListeners(Button button) => button.onClick.RemoveAllListeners();
    }
}