using System.Collections.Generic;
using UnityEngine;

namespace Libs.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup system configuration", order = 0)]
    public class PopupSystemConfiguration : ScriptableObject
    {
        [SerializeField] private int _startFromSortingOrder;
        [SerializeField] private List<Popup> _popups;
        [SerializeField] private Popup _startPopup;
        [SerializeField] private bool _spawnStartPopup;
        public List<Popup> Popups => _popups;
        public Popup StartPopup => _startPopup;
        public bool SpawnStartPopup => _spawnStartPopup;
        public int StartFromSortingOrder => _startFromSortingOrder;
    }
}