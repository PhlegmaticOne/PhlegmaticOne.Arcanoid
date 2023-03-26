using Libs.Services;
using Common.Packs.Views.Configurations;
using Common.Packs.Views.Views;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class PackPreviewFactoryInstaller : ServiceInstaller
    {
        [SerializeField] private PackPreview _prefab;
        [SerializeField] private PackPreviewConfiguration _notOpenedConfiguration;
        [SerializeField] private PackPreviewConfiguration _passedConfiguration;
        [SerializeField] private Sprite _notPassedLockSprite;
        [SerializeField] private string _notFoundLocalizationKey;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPackPreviewFactory>(
                new PackPreviewFactory(_prefab, _notOpenedConfiguration,
                    _passedConfiguration, _notPassedLockSprite, _notFoundLocalizationKey));
        }
    }
}