using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abstracts.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup system configuration", order = 0)]
    public class PopupSystemConfiguration : ScriptableObject
    {
        [SerializeField] private int _startFromSortingOrder;
        [SerializeField] private List<Popup> _popups;
        public List<Popup> Popups => _popups;
        public int StartFromSortingOrder => _startFromSortingOrder;
    }
}