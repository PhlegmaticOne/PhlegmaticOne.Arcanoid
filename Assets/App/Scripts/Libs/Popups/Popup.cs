using System;
using Libs.Pooling.Base;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete.Default;
using Libs.Popups.Animations.Types;
using Libs.Popups.Base;
using Libs.Popups.Configurations;
using Libs.Popups.View;
using UnityEngine;
using UnityEngine.UI;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Libs.Popups
{
    /// <summary>
    /// Popup lifetime pipeline:
    /// Initialize (InistializeProtected) -> OnShowed -> EnableInput -> ... -> DisableInput -> OnClosed -> Reset
    /// </summary>
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        [SerializeField] private PopupView _popupView;
        protected IPopupManager PopupManager;
        private IPopupAnimationsFactory<AppearAnimationType> _appearAnimationsFactory;
        private IPopupAnimationsFactory<DisappearAnimationType> _disappearAnimationsFactory;
        private Action _onCloseSpawnAction;

        private Action _onAnimationPlayedAction;
        
        private RectTransform _parentTransform;

        private PopupAnimationConfiguration _popupAnimationConfiguration;
        public PopupView PopupView => _popupView;
        public RectTransform RectTransform => transform as RectTransform;


        public void Initialize(IServiceProvider serviceProvider)
        {
            PopupManager = serviceProvider.GetRequiredService<IPopupManager>();
            InitializeProtected(serviceProvider);
        }
        
        public abstract void EnableInput();
        
        public abstract void DisableInput();
        
        protected abstract void InitializeProtected(IServiceProvider serviceProvider);

        protected virtual IPopupAnimation CreateCustomAppearAnimation() => new NoneAnimation();

        protected virtual IPopupAnimation CreateCustomDisappearAnimation() => new NoneAnimation();


        public void Show(int sortingOrder, Action onShowed)
        {
            _popupView.SetSortOrder(sortingOrder);

            var appearAnimation = _popupAnimationConfiguration.UsesCustomAppearAnimation ?
                    CreateCustomAppearAnimation() : 
                    CreateDefaultAppearAnimation();

            _onAnimationPlayedAction = () =>
            {
                appearAnimation.AnimationPlayed -= _onAnimationPlayedAction;
                onShowed?.Invoke();
                appearAnimation.Stop(this);
                OnShowed();
                EnableInput();
                _onCloseSpawnAction = null;
            };
            
            appearAnimation.AnimationPlayed += _onAnimationPlayedAction;
            appearAnimation.Play(this, _popupAnimationConfiguration.AppearanceTime);
        }
        

        public void Close(Action onCloseAction)
        {
            var disappearAnimation = _popupAnimationConfiguration.UsesCustomDisappearAnimation
                ? CreateCustomDisappearAnimation()
                : CreateDefaultDisappearAnimation();

            _onAnimationPlayedAction = () =>
            {
                disappearAnimation.AnimationPlayed -= _onAnimationPlayedAction;
                disappearAnimation.Stop(this);
                CloseInstant();
                onCloseAction?.Invoke();
                _onAnimationPlayedAction = null;
            };
            
            disappearAnimation.AnimationPlayed += _onAnimationPlayedAction;
            disappearAnimation.Play(this, _popupAnimationConfiguration.DisappearanceTime);
        }

        public void CloseInstant()
        {
            DisableInput();
            OnClosed();
        }
        
        public void SetPopupConfiguration(PopupConfiguration popupConfiguration)
        {
            _popupAnimationConfiguration = popupConfiguration.PopupAnimationConfiguration;
            _popupView.SetSortingLayer(popupConfiguration.SortingLayerName);
        }

        public void SetParentTransform(RectTransform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public void SetAnimationFactories(
            IPopupAnimationsFactory<AppearAnimationType> appearAnimationsFactory,
            IPopupAnimationsFactory<DisappearAnimationType> disappearAnimationsFactory)
        {
            _appearAnimationsFactory = appearAnimationsFactory;
            _disappearAnimationsFactory = disappearAnimationsFactory;
        }

        private IPopupAnimation CreateDefaultAppearAnimation() => 
            _appearAnimationsFactory.CreateAnimation(_popupAnimationConfiguration.AppearAnimationType, _parentTransform);
        private IPopupAnimation CreateDefaultDisappearAnimation() => 
            _disappearAnimationsFactory.CreateAnimation(_popupAnimationConfiguration.DisappearAnimationType, _parentTransform);

        protected virtual void OnShowed() { }

        protected virtual void OnClosed()
        {
            _onCloseSpawnAction?.Invoke();
            _onCloseSpawnAction = null;
        }
        
        public virtual void Reset() { }

        protected void OnCloseSpawn<TPopup>(Action<TPopup> withSetup = null) where TPopup : Popup
        {
            _onCloseSpawnAction = () =>
            {
                var popup = PopupManager.SpawnPopup<TPopup>();
                withSetup?.Invoke(popup);
            };
        }
        
        protected static void DisableBehaviour(Behaviour behaviour) => behaviour.enabled = false;
        protected static void EnableBehaviour(Behaviour behaviour) => behaviour.enabled = true;
        protected static void RemoveAllListeners(Button button) => button.onClick.RemoveAllListeners();
    }
}