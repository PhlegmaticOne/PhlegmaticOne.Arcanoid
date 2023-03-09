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

        public static IServiceProvider ServiceProvider
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

        public static void SetPrefabPath(string path) => _prefabPath = path;

        private void BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            
            foreach (var installer in _installers)
            {
                installer.InstallServices(serviceCollection);    
            }

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}