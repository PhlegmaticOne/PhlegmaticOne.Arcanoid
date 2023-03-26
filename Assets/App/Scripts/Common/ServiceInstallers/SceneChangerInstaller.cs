using System.Collections.Generic;
using Common.Scenes;
using Libs.Popups.Base;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class SceneChangerInstaller : ServiceInstaller
    {
        [SerializeField] private List<SceneInfo> _sceneInfos;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISceneChanger>(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var scenesProvider = new ScenesProvider(_sceneInfos);
                return new SceneChanger(popupManager, scenesProvider);
            });
        }
    }
}