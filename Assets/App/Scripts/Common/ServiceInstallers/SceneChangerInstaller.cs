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
        [SerializeField] private SceneChanger _sceneChanger;
        [SerializeField] private float _waitTime;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISceneChanger>(x =>
            {
                var sceneChanger = Instantiate(_sceneChanger);
                var popupManager = x.GetRequiredService<IPopupManager>();
                sceneChanger.Initialize(popupManager, new ScenesProvider(_sceneInfos));
                sceneChanger.SetParameters(_waitTime);
                return sceneChanger;
            });
        }
    }
}