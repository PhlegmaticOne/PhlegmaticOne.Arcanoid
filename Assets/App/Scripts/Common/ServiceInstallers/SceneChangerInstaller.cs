using Common.Scenes;
using Libs.Popups.Base;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class SceneChangerInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISceneChanger>(x => new SceneChanger(x.GetRequiredService<IPopupManager>()));
        }
    }
}