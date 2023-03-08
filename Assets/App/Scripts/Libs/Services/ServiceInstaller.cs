using UnityEngine;

namespace Libs.Services
{
    public abstract class ServiceInstaller : MonoBehaviour
    {
        public abstract void InstallServices(IServiceCollection serviceCollection);
    }
}