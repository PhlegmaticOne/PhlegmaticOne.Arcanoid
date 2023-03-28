using System;
using UnityEngine;

namespace Libs.Localization.Models
{
    [Serializable]
    public class LocaleInfo
    {
        [SerializeField] private string _displayName;
        [SerializeField] private string _systemName;
        public LocaleInfo(string systemName, string displayName)
        {
            _systemName = systemName;
            _displayName = displayName;
        }

        public string DisplayName => _displayName;
        public string SystemName => _systemName;
    }
}