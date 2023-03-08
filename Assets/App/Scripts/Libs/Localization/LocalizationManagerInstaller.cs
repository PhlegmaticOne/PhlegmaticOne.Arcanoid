using Libs.Localization.Base;
using Libs.Localization.Configurations;
using Libs.Localization.Implementations;
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
    }
}