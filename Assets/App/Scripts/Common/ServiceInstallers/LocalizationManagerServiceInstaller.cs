using Libs.Localization.Installers;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class LocalizationManagerServiceInstaller : ServiceInstaller
    {
        [SerializeField] private LocalizationManagerInstaller _localizationManagerInstaller;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_localizationManagerInstaller.CreateLocalizationManager());
        }
    }
}