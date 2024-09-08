using System.Collections.Generic;
using Libs.Localization.Installers.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Localization.Configurations
{
    [CreateAssetMenu(menuName = "Custom localization/Create localization system configuration", order = 0)]
    public class LocalizationSystemConfiguration : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<LocaleConfiguration, List<LocalizationTableInstallerBase>> _localizations;
        [SerializeField] private LocaleConfiguration _defaultLocale;

        public Dictionary<LocaleConfiguration, List<LocalizationTableInstallerBase>> Localizations => _localizations;
        public LocaleConfiguration DefaultLocale => _defaultLocale;
    }
}