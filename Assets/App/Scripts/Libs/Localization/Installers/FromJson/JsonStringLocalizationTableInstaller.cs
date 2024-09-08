using Libs.Localization.Base;
using Libs.Localization.Installers.Base;
using Libs.Localization.Tables.FromJson;
using UnityEngine;

namespace Libs.Localization.Installers.FromJson
{
    [CreateAssetMenu(menuName = "Custom localization/Installers/From json/Create string localization table installer", order = 0)]
    public class JsonStringLocalizationTableInstaller : LocalizationTableInstallerBase
    {
        [SerializeField] private TextAsset _jsonFile;
        public override ILocalizationTable CreateLocalizationTable() => 
            new JsonStringLocalizationTable(_jsonFile.text);
    }
}