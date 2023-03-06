using System;
using Libs.Pooling.Base;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Types;
using Libs.Popups.Configurations;
using Libs.Popups.View;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.Popups
{
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        [SerializeField] private PopupView _popupView;
        [SerializeField] private PopupConfiguration _popupConfiguration;
        private IPopupAnimationsFactory<AppearAnimationType> _appearAnimationsFactory;
        private IPopupAnimationsFactory<DisappearAnimationType> _disappearAnimationsFactory;
        private RectTransform _parentTransform;

        public PopupView PopupView => _popupView;
        public RectTransform RectTransform => transform as RectTransform;
        public PopupConfiguration PopupConfiguration => _popupConfiguration;
        
        public abstract void EnableInput();
        
        public abstract void DisableInput();
        
        public void Show(int sortingOrder, Action onShowed)
        {
            _popupView.SetSortOrder(sortingOrder);
            
            var popupAnimation = _appearAnimationsFactory
                .CreateAnimation(PopupConfiguration.AppearAnimationType, _parentTransform);
            
            popupAnimation.OnAnimationPlayed(() =>
            {
                onShowed?.Invoke();
                popupAnimation.Stop(this);
                EnableInput();
                OnShow();
            });
            
            popupAnimation.Play(this, PopupConfiguration.AppearanceTime);
        }
        
        public void Close(Action onCloseAction)
        {
            var popupAnimation = _disappearAnimationsFactory
                .CreateAnimation(PopupConfiguration.DisappearAnimationType, _parentTransform);
            
            popupAnimation.OnAnimationPlayed(() =>
            {
                onCloseAction?.Invoke();
                popupAnimation.Stop(this);
                CloseInstant();
            });
            
            popupAnimation.Play(this, PopupConfiguration.DisappearanceTime);
        }

        public void CloseInstant()
        {
            DisableInput();
            OnClosed();
        }

        public void SetParentTransform(RectTransform parentTransform) => _parentTransform = parentTransform;
        public void SetAnimationFactories(
            IPopupAnimationsFactory<AppearAnimationType> appearAnimationsFactory,
            IPopupAnimationsFactory<DisappearAnimationType> disappearAnimationsFactory)
        {
            _appearAnimationsFactory = appearAnimationsFactory;
            _disappearAnimationsFactory = disappearAnimationsFactory;
        }

        protected virtual void OnShow() { }
        protected virtual void OnClosed() { }
        
        public virtual void Reset() { }
        
        protected static void DisableBehaviour(Behaviour behaviour) => behaviour.enabled = false;
        protected static void EnableBehaviour(Behaviour behaviour) => behaviour.enabled = true;
        protected static void RemoveAllListeners(Button button) => button.onClick.RemoveAllListeners();
    }
}