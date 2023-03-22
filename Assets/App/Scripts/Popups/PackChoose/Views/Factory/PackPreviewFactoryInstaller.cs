using Libs.Services;
using Popups.PackChoose.Views.Configurations;
using UnityEngine;

namespace Popups.PackChoose.Views.Factory
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