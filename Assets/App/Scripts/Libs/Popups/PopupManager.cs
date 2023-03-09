using Libs.Pooling.Base;
using Libs.Popups.Base;
using Libs.Popups.Factory;
using Libs.Popups.Infrastructure;
using UnityEngine.Events;

namespace Libs.Popups
{
    public class PopupManager : IPopupManager
    {
        private readonly IPopupFactory _popupFactory;
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly StackList<Popup> _popups;

        private int _currentSortingOrder;

        public event UnityAction<Popup> PopupShowed;
        public event UnityAction<Popup> PopupClosed;
        public event UnityAction AllPopupsClosed;

        public PopupManager(IPopupFactory popupFactory,
            IPoolProvider poolProvider,
            int startFromSortingOrder)
        {
            _popupFactory = popupFactory;
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _currentSortingOrder = startFromSortingOrder;
            _popups = new StackList<Popup>();
        }

        public T SpawnPopup<T>() where T : Popup
        {
            var popup = _popupFactory.CreatePopup<T>();
            return (T)ShowPopup(popup);
        }

        public Popup SpawnPopup(Popup prefab)
        {
            var popup = _popupFactory.CreatePopup(prefab);
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