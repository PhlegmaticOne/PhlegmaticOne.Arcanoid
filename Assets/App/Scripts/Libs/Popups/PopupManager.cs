using System.Collections.Generic;
using Libs.Pooling.Base;
using Libs.Popups.Base;
using Libs.Popups.Factory;
using Libs.Popups.Infrastructure;

namespace Libs.Popups
{
    public class PopupManager : IPopupManager
    {
        private readonly IPopupFactory _popupFactory;
        private readonly IAbstractObjectPool<Popup> _popupsPool;
        private readonly StackList<Popup> _popups;

        private int _currentSortingOrder;

        public PopupManager(IPopupFactory popupFactory,
            IPoolProvider poolProvider,
            int startFromSortingOrder)
        {
            _popupFactory = popupFactory;
            _popupsPool = poolProvider.GetAbstractPool<Popup>();
            _currentSortingOrder = startFromSortingOrder;
            _popups = new StackList<Popup>();
        }

        public IList<Popup> GetAll() => _popups.ToList();

        public Popup CurrentPopup { get; private set; }

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

        public void CloseLastPopup(bool enablePreviousPopupInput = true)
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            var popup = _popups.Pop();
            CloseAnimate(popup, enablePreviousPopupInput);
        }

        public void ClosePopup(Popup popup, bool enablePreviousPopupInput = true)
        {
            if (_popups.Count == 0)
            {
                return;
            }
            
            _popups.Remove(popup);
            CloseAnimate(popup, enablePreviousPopupInput);
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
            var count = _popups.Count;
            
            while (count != 0)
            {
                CloseLastPopupInstant();
                count--;
            }
        }

        private Popup ShowPopup(Popup popup)
        {
            ++_currentSortingOrder;
            CurrentPopup = popup;
            
            if (_popups.Count != 0)
            {
                _popups.Peek().DisableInput();
            }

            _popups.Push(popup);
            popup.Show(_currentSortingOrder);
            return popup;
        }

        private void CloseAnimate(Popup popup, bool enablePreviousPopupInput)
        {
            --_currentSortingOrder;
            CurrentPopup = _popups.Count != 0 ? _popups.Peek() : null;
            popup.Close(() =>
            {
                _popupsPool.ReturnToPool(popup);
                
                if (_popups.Count != 0 && enablePreviousPopupInput)
                {
                    _popups.Peek().EnableInput();
                }
            });
        }

        private void CloseInstant(Popup popup)
        {
            --_currentSortingOrder;
            CurrentPopup = _popups.Count != 0 ? _popups.Peek() : null;
            popup.CloseInstant();
            _popupsPool.ReturnToPool(popup);

            // if (_popups.Count != 0)
            // {
            //     _popups.Peek().EnableInput();
            // }
        }
    }
}