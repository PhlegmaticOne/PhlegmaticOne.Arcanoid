using Libs.Localization.Base;
using Libs.Localization.Configurations;
using Libs.Localization.Implementations;
using Libs.Localization.Models;
using UnityEngine;

namespace Libs.Localization.Installers
{
    public class LocalizationManagerInstaller : MonoBehaviour
    {
        [SerializeField] private LocalizationSystemConfiguration _localizationSystemConfiguration;
        
        public ILocalizationManager CreateLocalizationManager()
        {
            return new LocalizationManager(_localizationSystemConfiguration);
        }
        
        public ILocalizationManager CreateLocalizationManager(LocaleInfo startLocale)
        {
            return new LocalizationManager(_localizationSystemConfiguration, startLocale);
        }
    }
}