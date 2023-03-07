using UnityEngine;

namespace Libs.Localization.Configurations
{
    [CreateAssetMenu(menuName = "Custom localization/Create locale", order = 0)]
    public class LocaleConfiguration : ScriptableObject
    {
        [SerializeField] private string _systemName;
        public string SystemName => _systemName;
    }
}