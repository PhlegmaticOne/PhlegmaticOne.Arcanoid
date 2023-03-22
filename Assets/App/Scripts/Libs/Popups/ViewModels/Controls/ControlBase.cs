using System;
using Libs.Popups.ViewModels.Actions;
using UnityEngine;

namespace Libs.Popups.Controls.Base
{
    public abstract class ControlBase : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private bool _isInteractable = true;
        private IControlAction _controlAction;
        public RectTransform RectTransform => _rectTransform;
        
        private Vector3 _awakePosition;
        private void Awake()
        {
            _awakePosition = _rectTransform.localPosition;
            Initialize();
        }

        public object ControlValue { get; set; }
        public bool IsInteractable
        {
            get => _isInteractable;
            set
            {
                _isInteractable = value;
                OnInteractableSet(_isInteractable);
            }
        }

        public void UpdateControl() => IsInteractable = _controlAction.CanExecute(ControlValue);

        public abstract void OnClick(Action<ControlBase> action);
        public abstract void Enable();
        public abstract void Disable();

        public void Reset()
        {
            _rectTransform.localPosition = _awakePosition;
            _controlAction = null;
            ResetProtected();
        }

        protected abstract void ResetProtected();

        public void BindToAction(IControlAction controlAction)
        {
            _controlAction = controlAction;
            UpdateControl();
            OnClick(c => _controlAction.Execute(c.ControlValue));
        }
        
        public void BindToActionWithValue(IControlAction controlAction, object controlValue)
        {
            _controlAction = controlAction;
            ControlValue = controlValue;
            UpdateControl();
            OnClick(c => _controlAction.Execute(c.ControlValue));
        }

        protected virtual void OnInteractableSet(bool isInteractable) { }
        protected virtual void Initialize() { }
        protected void SetInteractableDirect(bool interactable) => _isInteractable = interactable;
    }
}