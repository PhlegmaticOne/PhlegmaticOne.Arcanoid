using Libs.Localization.Base;
using Libs.Localization.Installers.Base;
using Libs.Localization.Tables.FromJson;
using UnityEngine;

namespace Libs.Localization.Installers.FromJson
{
    [CreateAssetMenu(menuName = "Custom localization/Installers/From json/Create sprite localization table installer", order = 0)]
    public class JsonSpriteLocalizationTableInstaller : LocalizationTableInstallerBase
    {
        [SerializeField] private TextAsset _jsonFile;
        public override ILocalizationTable CreateLocalizationTable() => 
            new JsonSpriteLocalizationTable(_jsonFile.text);
    }
}