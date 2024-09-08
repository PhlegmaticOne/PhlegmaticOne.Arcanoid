using Libs.Localization.Base;
using UnityEngine;

namespace Libs.Localization.Installers.Base
{
    public abstract class LocalizationTableInstallerBase : ScriptableObject
    {
        public abstract ILocalizationTable CreateLocalizationTable();
    }
}