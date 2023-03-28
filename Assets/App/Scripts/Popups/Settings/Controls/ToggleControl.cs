using System;
using Libs.Popups.Controls.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Settings.Controls
{
    public class ToggleControl : ControlBase
    {
        [SerializeField] private Toggle _toggle;
        public override void OnClick(Action<ControlBase> action)
        {
            _toggle.onValueChanged.AddListener(b =>
            {
                ControlValue = b;
                action(this);
            });
        }

        public void Initialize(bool value)
        {
            _toggle.isOn = value;
            ControlValue = value;
        }

        public override void Enable() => _toggle.enabled = true;

        public override void Disable() => _toggle.enabled = false;

        protected override void ResetProtected() => _toggle.onValueChanged.RemoveAllListeners();
    }
}