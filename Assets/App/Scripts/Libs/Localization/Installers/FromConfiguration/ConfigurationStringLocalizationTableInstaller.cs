using Libs.Localization.Base;
using Libs.Localization.Installers.Base;
using Libs.Localization.Tables.FromConfiguration;
using UnityEngine;

namespace Libs.Localization.Installers.FromConfiguration
{
    [CreateAssetMenu(menuName = "Custom localization/Installers/From configuration/Create string localization table installer", order = 0)]
    public class ConfigurationStringLocalizationTableInstaller : LocalizationTableInstallerBase
    {
        [SerializeField] private ConfigurationStringLocalizationTable _stringLocalizationTable;
        public override ILocalizationTable CreateLocalizationTable() => _stringLocalizationTable;
    }
}