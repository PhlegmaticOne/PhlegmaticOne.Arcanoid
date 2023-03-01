using Abstracts.Pooling.Implementation;
using Abstracts.Popups;
using Abstracts.Popups.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private Button _button;

        private IPopupManager _popupManager;
        private void Start()
        {
            var poolBuilder = PoolBuilder.Create();
            _popupComposite.AddPopupsToPool(poolBuilder);
            
            var poolProvider = poolBuilder.BuildProvider();
            _popupManager = _popupComposite.CreatePopupManager(poolProvider);
            
            _button.onClick.AddListener(() =>
            {
                _popupManager.SpawnPopup<ButtonPopup>(popup => popup.Initialize(_popupManager));
            });
        }
    }
}