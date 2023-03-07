using Libs.Localization.Base;
using Libs.Localization.Installers.Base;
using Libs.Localization.Tables.FromConfiguration;
using UnityEngine;

namespace Libs.Localization.Installers.FromConfiguration
{
    [CreateAssetMenu(menuName = "Custom localization/Installers/From configuration/Create sprite localization table installer", order = 0)]
    public class ConfigurationSpriteLocalizationTableInstaller : LocalizationTableInstallerBase
    {
        [SerializeField] private ConfigurationSpriteLocalizationTable _spritesLocalizationTable;
        public override ILocalizationTable CreateLocalizationTable() => _spritesLocalizationTable;
    }
}