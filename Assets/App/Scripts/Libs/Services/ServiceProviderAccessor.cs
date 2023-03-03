namespace Libs.Services
{
    public class ServiceProviderAccessor
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        
        public static IServiceProvider ServiceProvider => _serviceProvider;
    }
}