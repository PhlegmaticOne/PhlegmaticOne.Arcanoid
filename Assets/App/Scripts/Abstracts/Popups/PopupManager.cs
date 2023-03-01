using System;
using System.Collections.Generic;
using Abstracts.Pooling.Base;
using Abstracts.Popups.Animations.Base;
using Abstracts.Popups.Animations.Types;
using Abstracts.Popups.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Abstracts.Popups
{
    public class PopupManager : IPopupManager
    {
        private readonly IPopupAnimationsFactory<AppearAnimationType> _appearanceAnimationsFactory;
        private readonly IPopupAnimationsFactory<DisappearAnimationType> _disappearanceAnimationsFactory;
        private readonly RectTransform _mainCanvasTransform;
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly Stack<Popup> _popups;

        private int _currentSortingOrder;

        public event UnityAction<Popup> PopupShowed;
        public event UnityAction<Popup> PopupHid;
        public event UnityAction AllPopupsHid;

        public PopupManager(IPoolProvider poolProvider, 
            IPopupAnimationsFactory<AppearAnimationType> appearanceAnimationsFactory,
            IPopupAnimationsFactory<DisappearAnimationType> disappearanceAnimationsFactory,
            RectTransform mainCanvasTransform,
            int startFromSortingOrder)
        {
            _appearanceAnimationsFactory = appearanceAnimationsFactory;
            _disappearanceAnimationsFactory = disappearanceAnimationsFactory;
            _mainCanvasTransform = mainCanvasTransform;
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _popups = new Stack<Popup>();
            _currentSortingOrder = startFromSortingOrder;
        }

        public void SpawnPopup<T>(Action<T> initAction = null) where T : Popup
        {
            var popup = _popupsPool.GetConcrete<T>();
            initAction?.Invoke(popup);
            var conf = popup.PopupConfiguration;
            var animation = _appearanceAnimationsFactory.CreateAnimation(conf.AppearAnimationType, _mainCanvasTransform);
            
            animation.OnAnimationPlayed(() =>
            {
                animation.Stop(popup);
                OnPopupShowed(popup);
            });

            if (_popups.Count != 0)
            {
                _popups.Peek().DisableInput();
            }

            ++_currentSortingOrder;
            _popups.Push(popup);
            popup.Show(animation, _currentSortingOrder);
        }
        
        public void HidePopup()
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            var popup = _popups.Pop();
            var conf = popup.PopupConfiguration;
            var animation = _disappearanceAnimationsFactory.CreateAnimation(conf.DisappearAnimationType, _mainCanvasTransform);
            
            animation.OnAnimationPlayed(() =>
            {
                animation.Stop(popup);
                _popupsPool.ReturnToPool(popup);

                if (_popups.Count != 0)
                {
                    _popups.Peek().EnableInput();
                }
                
                OnHid(popup);
            });

            --_currentSortingOrder;
            popup.Hide(animation);
        }

        private void OnPopupShowed(Popup popup) => PopupShowed?.Invoke(popup);
        private void OnPopupHid(Popup popup) => PopupHid?.Invoke(popup);
        private void OnAllPopupsHid() => AllPopupsHid?.Invoke();

        private void OnHid(Popup popup)
        {
            OnPopupHid(popup);

            if (_popups.Count == 0)
            {
                OnAllPopupsHid();
            }
        }
    }
}