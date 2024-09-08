using Libs.Pooling.Base;
using Libs.Popups;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class PopupManagerInstaller : ServiceInstaller
    {
        [SerializeField] private PopupComposite _popupComposite;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(s => 
                _popupComposite.CreatePopupManager(s.GetRequiredService<IPoolProvider>()));
        }
    }
}