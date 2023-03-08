using System.Collections.Generic;
using UnityEngine;

namespace Libs.Services
{
    public class ServiceProviderAccessor : MonoBehaviour
    {
        [SerializeField] private List<ServiceInstaller> _installers;
        private IServiceProvider _serviceProvider;
        private static ServiceProviderAccessor _accessor;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_accessor == null)
                {
                    var existing = FindObjectOfType<ServiceProviderAccessor>();
                    
                    if (existing == false)
                    {
                        return null;
                    }
                    
                    existing.BuildServiceProvider();
                    DontDestroyOnLoad(existing);
                    _accessor = existing;
                }

                return _accessor._serviceProvider;
            }
        }
        
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