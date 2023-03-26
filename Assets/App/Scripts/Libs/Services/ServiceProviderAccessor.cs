using System;
using System.Collections.Generic;
using UnityEngine;

namespace Libs.Services
{
    public class ServiceProviderAccessor : MonoBehaviour
    {
        [SerializeField] private List<ServiceInstaller> _installers;
        private IServiceProvider _serviceProvider;
        private static string _prefabPath;
        private static ServiceProviderAccessor _accessor;
        private Dictionary<string, IServiceProvider> _sceneServiceProviders = new Dictionary<string, IServiceProvider>();

        public static IServiceProvider Global
        {
            get
            {
                if (_accessor == null)
                {
                    var existing = Resources.Load<ServiceProviderAccessor>(_prefabPath);
                    existing.BuildServiceProvider();
                    DontDestroyOnLoad(existing);
                    _accessor = existing;
                }

                return _accessor._serviceProvider;
            }
        }

        public static object SearchService(Type serviceType)
        {
            var result = Global.GetService(serviceType);
            
            if (result != null)
            {
                return result;
            }

            foreach (var sceneServiceProvider in Instance._sceneServiceProviders)
            {
                var serviceProvider = sceneServiceProvider.Value;
                result = serviceProvider.GetService(serviceType);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        
        public static ServiceProviderAccessor Instance => _accessor;

        public static void SetPrefabPath(string path) => _prefabPath = path;

        public void AddSceneServiceProvider(string sceneKey, List<ServiceInstaller> serviceInstallers)
        {
            var serviceProvider = BuildServiceProvider(serviceInstallers);
            _sceneServiceProviders.Add(sceneKey, serviceProvider);
        }

        public void RemoveSceneServiceProvider(string sceneKey) => _sceneServiceProviders.Remove(sceneKey);

        public IServiceProvider ForScene(string sceneKey) => _sceneServiceProviders[sceneKey];

        private void BuildServiceProvider() => _serviceProvider = BuildServiceProvider(_installers);

        private static IServiceProvider BuildServiceProvider(List<ServiceInstaller> serviceInstallers)
        {
            var serviceCollection = new ServiceCollection();
            
            foreach (var installer in serviceInstallers)
            {
                installer.InstallServices(serviceCollection);    
            }

            return serviceCollection.BuildServiceProvider();
        }
    }
}