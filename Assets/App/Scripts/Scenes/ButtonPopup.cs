using Abstracts.Popups;
using Abstracts.Popups.Base;
using Abstracts.Popups.View;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes
{
    public class ButtonPopup : Popup
    {
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;
        private IPopupManager _popupManager;

        public void Initialize(IPopupManager popupManager)
        {
            _popupManager = popupManager;
            _button1.onClick.AddListener(() =>
            {
                _popupManager.HidePopup();
            });
            _button2.onClick.AddListener(() => _popupManager.SpawnPopup<ButtonPopup>(p => p.Initialize(_popupManager)));
            _button3.onClick.AddListener(DisableInput);
        }

        public override void EnableInput()
        {
            _button1.enabled = true;
            _button2.enabled = true;
        }

        public override void DisableInput()
        {
            _button1.enabled = false;
            _button2.enabled = false;
        }

        public override void Reset()
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
        }
    }
}