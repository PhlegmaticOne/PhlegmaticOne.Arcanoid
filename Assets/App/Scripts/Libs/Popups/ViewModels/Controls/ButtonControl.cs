using System;
using Libs.Popups.Controls.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.Popups.Controls
{
    public class ButtonControl : ControlBase
    {
        [SerializeField] private Button _button;
        public override void OnClick(Action<ControlBase> action)
        {
            _button.onClick.AddListener(() =>
            {
                if (IsInteractable == false)
                {
                    return;
                } 
                action(this);
            });
        }

        public override void Enable() => _button.enabled = true;
        public override void Disable() => _button.enabled = false;
        protected override void ResetProtected() => _button.onClick.RemoveAllListeners();
    }
}