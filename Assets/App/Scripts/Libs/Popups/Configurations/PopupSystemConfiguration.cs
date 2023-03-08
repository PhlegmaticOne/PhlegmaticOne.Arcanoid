using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Libs.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup system configuration", order = 0)]
    public class PopupSystemConfiguration : ScriptableObject
    {
        [SerializeField] private int _startFromSortingOrder;
        [SerializeField] private List<PopupConfiguration> _popups;
        [SerializeField] private PopupConfiguration _startPopup;
        [NonSerialized] private bool _spawnStartPopup = true;
        
        public List<PopupConfiguration> Popups => _popups;
        public PopupConfiguration StartPopup => _startPopup;
        public bool SpawnStartPopup => _spawnStartPopup;
        public int StartFromSortingOrder => _startFromSortingOrder;

        public PopupConfiguration FindConfigurationForPrefab(Popup popup)
        {
            return _popups.First(x => x.Popup.GetType() == popup.GetType());
        }

        public void DisableStartPopupSpawn() => _spawnStartPopup = false;
    }
}