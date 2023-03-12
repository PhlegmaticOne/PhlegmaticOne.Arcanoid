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
        private Dictionary<int, IServiceProvider> _sceneServiceProviders = new Dictionary<int, IServiceProvider>();

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

        public static ServiceProviderAccessor Instance => _accessor;

        public static void SetPrefabPath(string path) => _prefabPath = path;

        public void AddSceneServiceProvider(int sceneIndex, List<ServiceInstaller> serviceInstallers)
        {
            var serviceProvider = BuildServiceProvider(serviceInstallers);
            _sceneServiceProviders.Add(sceneIndex, serviceProvider);
        }

        public void RemoveSceneServiceProvider(int sceneIndex) => _sceneServiceProviders.Remove(sceneIndex);

        public IServiceProvider ForScene(int sceneIndex) => _sceneServiceProviders[sceneIndex];

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