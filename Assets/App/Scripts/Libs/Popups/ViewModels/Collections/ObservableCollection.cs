using System;
using System.Collections.Generic;
using Libs.Popups.Controls.Base;

namespace Libs.Popups.ViewModels.Collections
{
    public class ObservableCollection<TModel, TView> 
        where TModel : class
        where TView : ControlBase
    {
        private Func<TModel, TView> _bindingFunc;
        private readonly Func<TModel, bool> _isInteractableBinding;
        private Action<TView> _destroyAction;
        private readonly List<TView> _views;

        private bool _isEnabled;

        public ObservableCollection(Func<TModel, TView> bindingFunc, 
            Func<TModel, bool> isInteractableBinding,
            Action<TView> destroyAction)
        {
            _bindingFunc = bindingFunc;
            _isInteractableBinding = isInteractableBinding;
            _destroyAction = destroyAction;
            _isEnabled = true;
            _views = new List<TView>();
        }

        public List<TView> Views => _views;

        public event Action<TView, TModel> ItemClicked; 

        public void AddRange(IEnumerable<TModel> models)
        {
            foreach (var model in models)
            {
                var view = _bindingFunc(model);
                view.IsInteractable = _isInteractableBinding(model);
                view.OnClick(ViewOnClicked);
                view.ControlValue = model;
                _views.Add(view);
            }
        }

        public void Enable()
        {
            foreach (var view in _views)
            {
                view.Enable();
            }
            _isEnabled = true;
        }

        public void Disable()
        {
            foreach (var view in _views)
            {
                view.Disable();
            }
            _isEnabled = false;
        }

        public void Clear()
        {
            foreach (var view in _views)
            {
                view.Reset();
                _destroyAction(view);
            }
            _views.Clear();
            _bindingFunc = null;
            _destroyAction = null;
        }

        private void ViewOnClicked(ControlBase clicked)
        {
            if (_isEnabled == false)
            {
                return;
            }
            
            ItemClicked?.Invoke((TView)clicked, (TModel)clicked.ControlValue) ;
        }
    }
}