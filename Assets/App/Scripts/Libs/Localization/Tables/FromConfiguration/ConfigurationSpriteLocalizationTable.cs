using Libs.Localization.Tables.FromConfiguration.Base;
using UnityEngine;

namespace Libs.Localization.Tables.FromConfiguration
{
    [CreateAssetMenu(menuName = "Custom localization/Tables/Create sprite localization table", order = 0)]
    public class ConfigurationSpriteLocalizationTable : ConfigurationLocalizationTableBase<Sprite> { }
}