using Libs.Pooling.Base;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Types;
using Libs.Popups.Base;
using Libs.Popups.Infrastructure;
using Libs.Popups.Initialization;
using UnityEngine;
using UnityEngine.Events;

namespace Libs.Popups
{
    public class PopupManager : IPopupManager
    {
        private readonly IPopupAnimationsFactory<AppearAnimationType> _appearAnimationsFactory;
        private readonly IPopupAnimationsFactory<DisappearAnimationType> _disappearAnimationsFactory;
        private readonly IPopupInitializersProvider _popupInitializersProvider;
        private readonly RectTransform _mainCanvasTransform;
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly StackList<Popup> _popups;

        private int _currentSortingOrder;

        public event UnityAction<Popup> PopupShowed;
        public event UnityAction<Popup> PopupClosed;
        public event UnityAction AllPopupsClosed;

        public PopupManager(IPoolProvider poolProvider, 
            IPopupAnimationsFactory<AppearAnimationType> appearAnimationsFactory,
            IPopupAnimationsFactory<DisappearAnimationType> disappearAnimationsFactory,
            IPopupInitializersProvider popupInitializersProvider,
            RectTransform mainCanvasTransform,
            int startFromSortingOrder)
        {
            _appearAnimationsFactory = appearAnimationsFactory;
            _disappearAnimationsFactory = disappearAnimationsFactory;
            _popupInitializersProvider = popupInitializersProvider;
            _mainCanvasTransform = mainCanvasTransform;
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _popups = new StackList<Popup>();
            _currentSortingOrder = startFromSortingOrder;
        }

        public T SpawnPopup<T>() where T : Popup
        {
            var popup = _popupsPool.GetConcrete<T>();
            return (T)ShowPopup(popup);
        }

        public Popup SpawnPopup(Popup prefab)
        {
            var popup = _popupsPool.GetByType(prefab.GetType());
            return ShowPopup(popup);
        }

        public void CloseLastPopup()
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            var popup = _popups.Pop();
            CloseAnimate(popup);
        }

        public void ClosePopup(Popup popup)
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            _popups.Remove(popup);
            CloseAnimate(popup);
        }

        public void CloseLastPopupInstant()
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            var popup = _popups.Pop();
            CloseInstant(popup);
        }

        public void ClosePopupInstant(Popup popup)
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            _popups.Remove(popup);
            CloseInstant(popup);
        }

        public void CloseAllPopupsInstant()
        {
            while (_popups.Count != 0)
            {
                CloseLastPopupInstant();
            }
        }

        private Popup ShowPopup(Popup popup)
        {
            _popupInitializersProvider.InitializePopup(popup);
            popup.SetParentTransform(_mainCanvasTransform);
            popup.SetAnimationFactories(_appearAnimationsFactory, _disappearAnimationsFactory);
            
            ++_currentSortingOrder;
            if (_popups.Count != 0)
            {
                _popups.Peek().DisableInput();
            }

            _popups.Push(popup);
            
            popup.Show(_currentSortingOrder, () =>
            {
                OnPopupShowed(popup);
            });

            return popup;
        }

        private void CloseAnimate(Popup popup)
        {
            --_currentSortingOrder;
            popup.Close(() => OnClosedActions(popup));
        }

        private void CloseInstant(Popup popup)
        {
            --_currentSortingOrder;
            popup.CloseInstant();
            OnClosedActions(popup);
        }

        private void OnClosedActions(Popup popup)
        {
            _popupsPool.ReturnToPool(popup);
            
            if (_popups.Count != 0)
            {
                _popups.Peek().EnableInput();
            }
            
            OnClosed(popup);
        }
        
        private void OnClosed(Popup popup)
        {
            OnPopupClosed(popup);

            if (_popups.Count == 0)
            {
                OnAllPopupsClosed();
            }
        }
        
        private void OnPopupShowed(Popup popup) => PopupShowed?.Invoke(popup);
        private void OnPopupClosed(Popup popup) => PopupClosed?.Invoke(popup);
        private void OnAllPopupsClosed() => AllPopupsClosed?.Invoke();
    }
}