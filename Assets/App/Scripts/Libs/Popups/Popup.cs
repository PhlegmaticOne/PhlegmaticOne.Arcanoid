using System;
using Libs.Pooling.Base;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Base;
using Libs.Popups.Configurations;
using Libs.Popups.View;
using UnityEngine;

namespace Libs.Popups
{
    /// <summary>
    /// Popup lifetime pipeline:
    /// Initialize (InitializeProtected) -> OnShowed -> EnableInput -> ... -> DisableInput -> OnClosed -> Reset
    /// </summary>
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        [SerializeField] protected PopupView _popupView;

        private Action _onAnimationPlayedAction;
        
        protected RectTransform ParentTransform;

        public PopupView PopupView => _popupView;
        public RectTransform RectTransform => transform as RectTransform;

        public abstract void EnableInput();
        
        public abstract void DisableInput();
        
        protected virtual IPopupAnimation CreateCustomAppearAnimation() => Animate.None();

        protected virtual IPopupAnimation CreateCustomDisappearAnimation() => Animate.None();


        public void Show(int sortingOrder)
        {
            _popupView.SetSortOrder(sortingOrder);
            
            OnBeforeShowing();
            DisableInput();

            var appearAnimation = CreateCustomAppearAnimation();

            _onAnimationPlayedAction = () =>
            {
                appearAnimation.AnimationPlayed -= _onAnimationPlayedAction;
                appearAnimation.Stop();
                OnShowed();
                EnableInput();
                _onAnimationPlayedAction = null;
            };
            
            appearAnimation.AnimationPlayed += _onAnimationPlayedAction;
            appearAnimation.Play();
        }
        
        public void Close(Action onClose)
        {
            OnBeforeClosing();
            
            var disappearAnimation = CreateCustomDisappearAnimation();
            
            _onAnimationPlayedAction = () =>
            {
                disappearAnimation.AnimationPlayed -= _onAnimationPlayedAction;
                disappearAnimation.Stop();
                CloseInstant();
                onClose?.Invoke();
                _onAnimationPlayedAction = null;
            };
            
            disappearAnimation.AnimationPlayed += _onAnimationPlayedAction;
            disappearAnimation.Play();
        }
        
        public void CloseInstant()
        {
            DisableInput();
            OnClosed();
        }

        protected void ToZeroPosition() => RectTransform.localPosition = Vector3.zero;
        
        protected virtual void OnBeforeShowing() { }

        protected virtual void OnBeforeClosing() { }
        
        
        public void SetPopupConfiguration(PopupConfiguration popupConfiguration) => 
            _popupView.SetSortingLayer(popupConfiguration.SortingLayerName);

        public void SetParentTransform(RectTransform parentTransform) => 
            ParentTransform = parentTransform;

        protected virtual void OnShowed() { }

        protected virtual void OnClosed() { }
        
        public virtual void Reset() { }
    }
}